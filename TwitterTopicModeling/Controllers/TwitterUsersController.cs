namespace TwitterTopicModeling.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.EntityFrameworkCore;
    using System.Threading;
    using System.Collections;
    using TwitterTopicModeling.Database;
    using TwitterTopicModeling.Database.Models;



    // internal
    using Services;
    

    [ApiController]
    [Route("[controller]")]
    public class TwitterUsersController : ControllerBase
    {
        public ILogger<TwitterUsersController> Logger { get; }
        public TwitterService TwitterService { get; }
        public TwitterContext TwitterContext { get; }
    
        public TwitterUsersController(ILogger<TwitterUsersController> logger, TwitterService twitterService, TwitterContext twitterContext)
        {
            Logger = logger;
            TwitterService = twitterService;
            TwitterContext = twitterContext;
        }


        //This endpint will accept the a username from the fron end       
        // then is will a check to see if its in our database if so it returns
        // if not it will request the user fromn the twitter api put it in the database and save the changes
        [HttpGet("{username}")]
        public async Task<TwitterUser> getTwitterUser(string username)
        {

            return await TwitterService.getTwitterUser(username);

        }


        //method for getting a user's timeline of tweets
        //count specifies how many
        [HttpGet("{userName}/tweets/{count}")]
        public async Task<List<Tweet>> getTweets(string userName, int count)
        {
            
            return await TwitterService.GetTweets(userName, count);


        }
    }



}