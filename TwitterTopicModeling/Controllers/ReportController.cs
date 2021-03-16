namespace TwitterTopicModeling.Controllers
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;


    using Flurl;
    using Flurl.Http;

    using System.Threading;
    using System.Collections;
    using TwitterTopicModeling.Database;
    using TwitterTopicModeling.Database.Models;

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
        public async Task<Report> generateReport(ReportDTO report)
        {

            int count = 50;
            var T = await TwitterService.GetTweets(report.username, count);

            //need to do type conversion because the service returns a twitter model not a database model
            var timeline = T.Select(x => new Tweet
            {
                ExternalId = x.Id,
                Text = x.Text

            }).ToList();

            await TwitterContext.SaveChangesAsync();
            return null;
        }

        [HttpGet("getReport/{username}")]
        public async Task<Report> getUser(string UserName)
        {

            //ensuere twitterUser in database first


            var Report = await TwitterContext.Report
           .Where(x => x.TwitterUser.ScreenName == UserName)
           .FirstOrDefaultAsync();

            return Report;

        }

    }
}