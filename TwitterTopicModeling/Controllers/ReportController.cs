namespace TwitterTopicModeling.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.IO;
    using System.Globalization;
    using System.Diagnostics;
    using Microsoft.Extensions.Configuration;



    using Flurl;
    using Flurl.Http;

    using System.Threading;
    using System.Collections;
    using TwitterTopicModeling.Database;
    using TwitterTopicModeling.Database.Models;
    using CsvHelper;

    // internal
    using Services;
    using Microsoft.EntityFrameworkCore;
    using TwitterTopicModeling.Payloads;
    using TwitterTopicModeling.Utils;


    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {

        public ILogger<TwitterUsersController> Logger { get; }
        public TwitterService TwitterService { get; }
        public TwitterContext TwitterContext { get; }
        public string Rscript { get; }

        //this path is to the location that the r exe file for runing a script in the backround it 
        public string exeRpath { get; }

        //malicious words "words we are looking for"
        string[] malWords = { "arrested", "Murder"};


        public ReportController(ILogger<TwitterUsersController> logger, TwitterService twitterService, TwitterContext twitterContext, IConfiguration configuration)
        {
            Logger = logger;
            TwitterService = twitterService;
            TwitterContext = twitterContext;
            Rscript = configuration["Rscript"];
            exeRpath = configuration["exeRpath"];
        }


        [HttpPost("generateReport")]
        public async Task<object> generateReport([FromBody] ReportDTO report, [FromHeader(Name = "user-id")] int userId)
        {


            //getting the user that is logged in using the user-id that is passed in the header of the request
            var user = await TwitterContext.Users
                 .Where(x => x.Id == userId)
                 .FirstOrDefaultAsync();

            //getting twitter user for report
            var twitterUser = await TwitterService.getTwitterUser(report.username);




            //check to see if user is in our database or is an existing user in the Twitter API
            await TwitterService.getTwitterUser(report.username);

            //getting the timeline for the user from the twitterAPI
            var collectedTweets = await TwitterService.GetTweets(report.username, report.count);

            //making temp driectory for temp csvs 
            //https://gist.github.com/JoeHartzell/ab6ebd4af690c79e84c728f5da367dcc
            using var tempDir = TempDir.Create();

            //writing the tweets tp a csv so the tr script can accept it as an argument 
            using var writer = new StreamWriter($"{tempDir.Name}\\tweets.csv");
            //URL for csv Helper
            //https://joshclose.github.io/CsvHelper/
            using var csvWrite = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWrite.WriteRecords(collectedTweets);


            //runing r script on csv
            //URLs below is the locaiton of where this code was used from
            //https://stackoverflow.com/questions/14021055/r-script-form-c-passing-arguments-running-script-retrieving-results/44122606 
            //https://stackoverflow.com/questions/4485943/executing-r-script-programmatically=
            var result = "";
            try
            {
                var info = new ProcessStartInfo();
                info.FileName = exeRpath;
                info.WorkingDirectory = Path.GetDirectoryName(exeRpath);
                info.Arguments = Rscript + " " + $"{tempDir.Name}\\tweets.csv" + " " + $"{tempDir.Name}\\output.csv";

                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;


                using (var tempProcces = new Process())
                {
                    tempProcces.StartInfo = info;
                    tempProcces.Start();
                    result = tempProcces.StandardOutput.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("running the r script failed" + result, exception);
            }

            //read output csv that is in the tempDir location to a list
            using var reader = new StreamReader($"{tempDir.Name}\\output.csv");
            using var csvRead = new CsvReader(reader, CultureInfo.InvariantCulture);

            var topics = csvRead.GetRecords<ReportTopic>().ToList();

            var threshold = (int)Math.Ceiling(topics.Count() * .1);

            var flagTopics = topics
                .Take(threshold)
                .Select(x => x.Topic);


            //checks to see if any of topics aer listed in the malicious words array
            //if so it marks the report for malicious content
            var maliciousFlag = false;
            for(var x = 0; x < topics.Count && !maliciousFlag; x++)
            {
                for(var y = 0; y < malWords.Length && !maliciousFlag; y++)
                {
                    maliciousFlag = topics[x].Topic.Equals(malWords[y],StringComparison.OrdinalIgnoreCase);
                }
            }


            //creating the list of report tweets using linq 
            var reporttweets = collectedTweets
                .Select(tweet =>
                {
                    var flagged = flagTopics.Any(topic => tweet.Text.Contains(topic));
                    return new Report_tweet
                    {
                        Tweet = tweet,
                        flag = flagged
                    };
                })
                .ToList();


            //creating the new report
            Report currReport = new Report
            {

                ReportName = $"{report.username}",
                User = user,
                malFlag = maliciousFlag,
                TwitterUser = twitterUser,
                ReportTweets = reporttweets,
                Topics = topics,
            };


            await TwitterContext.Report.AddAsync(currReport);
            await TwitterContext.SaveChangesAsync();


            //has to be a generateReportDTO because there are nested objects and the recusion causes cycle error
            GenerateReportDTO rtnReport = new GenerateReportDTO
            {
                id = currReport.id,
                User = currReport.User,
                malFlag = currReport.malFlag,
                ReportName = currReport.ReportName,
                CreatedAt = currReport.CreatedAt,
                TwitterUser = currReport.TwitterUser,
                Topics = currReport.Topics,
                ReportTweets = currReport.ReportTweets
                    .Where(x => x.flag)
                    .Select(x => x.Tweet).ToList()
            };
            return rtnReport;
        }



        [HttpGet("getReportList")]
        public async Task<List<GenerateReportDTO>> GetReportList([FromHeader(Name = "user-id")] int userId)
        {

            //ensuere twitterUser in database first
            var ReportList = await TwitterContext.Report
           .Where(x => x.User.Id == userId)
           .Select(report => new GenerateReportDTO
           {
               id = report.id,
               User = report.User,
               ReportName = report.ReportName,
               malFlag = report.malFlag,
               CreatedAt = report.CreatedAt,
               TwitterUser = report.TwitterUser,
               Topics = report.Topics,
               ReportTweets = report.ReportTweets
                    .Where(x => x.flag)
                    .Select(x => x.Tweet).ToList()
           })
           .ToListAsync();

            return ReportList;

        }

        [HttpGet("getReport/{reportId}")]
        public async Task<IActionResult> getReport([FromHeader(Name = "user-id")] int userId, int reportId)
        {
            var report = await TwitterContext.Report
           .Where(x => x.User.Id == userId && x.id == reportId)
           .Select(report => new GenerateReportDTO
           {
               id = report.id,
               User = report.User,
               ReportName = report.ReportName,
               malFlag = report.malFlag,
               CreatedAt = report.CreatedAt,
               TwitterUser = report.TwitterUser,
               Topics = report.Topics,
               ReportTweets = report.ReportTweets
                    .Where(x => x.flag)
                    .Select(x => x.Tweet).ToList()
           })
           .FirstOrDefaultAsync();


            if(report is null)
            {
                return NotFound();
            }

            return Ok(report);
        }

    }
}