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


    
    }
}