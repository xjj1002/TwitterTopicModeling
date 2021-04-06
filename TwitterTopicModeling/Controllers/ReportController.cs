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


    [ApiController]
    [Route("[controller]")]
    public class ReportController
    {

        public ILogger<TwitterUsersController> Logger { get; }
        public TwitterService TwitterService { get; }
        public TwitterContext TwitterContext { get; }

        public ReportController(ILogger<TwitterUsersController> logger, TwitterService twitterService, TwitterContext twitterContext)
        {
            Logger = logger;
            TwitterService = twitterService;
            TwitterContext = twitterContext;
        }


        [HttpPost("generateReport")]
        public async Task<Report> generateReport([FromBody] ReportDTO report, [FromHeader(Name="user-id")] int userId)
        {


            Report currReport = new Report{

                ReportName = $"{report.username}",
                User = new User {
                    Id = userId
                }
            };

            
            //check to see if user is in our database or is an existing user in the Twitter API
             await TwitterService.getTwitterUser(report.username);

            //getting the timeline for the user from the twitterAPI
            int count = 50;
            var T = await TwitterService.GetTweets(report.username, count);


            using var writer = new StreamWriter("C:\\Projects\\TwitterTopicModeling\\TwitterTopicModeling\\test.csv");
            using var csv = new CsvWriter(writer,CultureInfo.InvariantCulture);
            csv.WriteRecords(T);

            await TwitterContext.SaveChangesAsync();

            return null;
        }

        [HttpGet("getReport/{username}")]
        public async Task<Report> GetReport(string UserName)
        {

            //ensuere twitterUser in database first
            var Report = await TwitterContext.Report
           .Where(x => x.TwitterUser.ScreenName == UserName)
           .FirstOrDefaultAsync();


            return Report;

        }

        [HttpGet("{userId}")]
        public async Task<List<Report>> GetReportList(int userId)
        {

            //ensuere twitterUser in database first
            var ReportList = await TwitterContext.Report
           .Where(x => x.User.Id == userId)
           .ToListAsync();

            return ReportList;

        }

    }
}