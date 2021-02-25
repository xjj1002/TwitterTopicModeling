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
  public class TwitterUsersController : ControllerBase
  {
    public ILogger<TwitterUsersController> Logger { get; }

    //twitter context attribute here
    public TwitterService TwitterService { get; }

    public TwitterContext TwitterContext { get; }


    //         
    //         
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


      //getting the user fromt he database if it is not there sets to null
      var DatabaseUser = await TwitterContext.TwitterUsers
          .Where(x => x.ScreenName == username)
          .FirstOrDefaultAsync();

      //checks to see if the user was in the database
      //if not goes to twitter api to get it
      if (DatabaseUser is null)
      {
        //if this fails what do i do?????????
        var user = await TwitterService.getTwitterUser(username);

        //if the twitter user exists then do this 
        if (user is not null)
        {
          var insertedUser = await TwitterContext.TwitterUsers
              .AddAsync(new TwitterUser
              {
                ScreenName = user.ScreenName
              });

          await TwitterContext.SaveChangesAsync();
          return insertedUser.Entity;
        }
      }

      Logger.LogInformation("Returns the user we got from the twitter API or from our database");

      return DatabaseUser;

    }

    [HttpGet("{userName}/tweets")]
    public async Task<Tweet> getTweets(string userName, int count = 50)
    {

      if (count > 100)
      {
        throw new Exception();
      }

      //     var DatabaseUser = await TwitterContext.Users
      // .Where(x => x.ScreenName == username)
      // .FirstOrDefaultAsync();
      //  // List<Tweet> timeline = await TwitterService.GetTwitterUserTimeline(userName, count);

      //     Logger.LogInformation("Returning the tweets that we got from he twitter API");
      //    // return timeline;

      throw new NotImplementedException();
    }
  }



}