
//serviec for addressing the Twitter Develper API endpoints
namespace TwitterTopicModeling.Services
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;


    using System.Threading.Tasks;

    using System.Collections.Generic;
    using System.Linq;

    using TwitterTopicModeling.Database;
    using TwitterTopicModeling.Database.Models;
    using Microsoft.EntityFrameworkCore;


    //using for Furl Http request wrapper
    //linke to FLurl github https://github.com/tmenier/Flurl
    using Flurl;
    using Flurl.Http;

  public class TwitterService
  {


    public ILogger<TwitterService> Logger { get; }
    public string TwitterToken { get; }
    public string baseUrl = "https://api.twitter.com/1.1";
    public TwitterContext TwitterContext { get; }

    public TwitterService(ILogger<TwitterService> logger, IConfiguration configuration, TwitterContext twitterContext)
    {
      Logger = logger;
      TwitterToken = configuration["Twitter:BearerToken"];
      TwitterContext = twitterContext;
      

    }

    public async Task<List<Tweet>> GetTweets(string ScreenName, int count)
    {
        //checkes the database to see if the user exist there
        var DatabaseUser = await TwitterContext.TwitterUsers
            .Where(x => x.ScreenName == ScreenName)
            .FirstOrDefaultAsync();

        //if there is no user in the database we do not want the rest of the endpoint to run so it throws an error 
        //if the user is null it may not exist. the getTwitterUser endpoint must run before this endpoint or it will error every time
        if(DatabaseUser == null)
        {
            throw new System.Exception(message:"User not found in database");
        }


        //making the request to the twitter API for the user timeline
        var result = await baseUrl
            .AppendPathSegment("statuses/user_timeline.json")
            .WithOAuthBearerToken(TwitterToken)
            .SetQueryParams(new
            {
                screen_name = ScreenName,
                count
            })
            .GetJsonAsync<List<Twitter.Models.Tweet>>();


        //checking database for existing tweets for the user
        var tweetExtId = result.Select(x => x.Id);
        var dataBasetweet = await TwitterContext.Tweets
            .Where(x => tweetExtId.Contains(x.ExternalId))
            .ToListAsync();
        
        //filtering the existing tweets out to prevent duplicates
        //also trandforms the twitter models into databse models
        var inserts = result
            .Where(x => dataBasetweet.All(y => y.ExternalId != x.Id))
            .Select(tweet => new Tweet { 

                ExternalId = tweet.Id,
                Text = tweet.Text,
                TwitterUser = DatabaseUser,
                CreatedAt = tweet.CreatedAt,

            });


       
        var inserted = new List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Tweet>>();

        //checks to see if there are any new tweets to be inserted if not it skips the insert step
        if (inserts.Any())
        {
            
            //markes each database model tweet to be inserted
            foreach(var tweet in  inserts)
            {   
                var addedTweet = await TwitterContext.Tweets.AddAsync(tweet);
                inserted.Add(addedTweet);
            }

            //saves the makred tweets to the database
            await TwitterContext.SaveChangesAsync();
        }
        
        Logger.LogInformation("Getting twitter user timeline");

        //returning the existing tweets along with the new tweets
        return inserted
            .Select(x => x.Entity)
            .Union(dataBasetweet)
            .OrderBy(x => x.Id)
            .ToList();

    }



    //service now handles checking the database for the user
    //The controller used to do this but was waste of time 
    public async Task<TwitterUser> getTwitterUser(string userName)
    {

        //getting the user fromt he database if it is not there sets to null
        var DatabaseUser = await TwitterContext.TwitterUsers
            .Where(x => x.ScreenName == userName)
            .FirstOrDefaultAsync();

        //checks to see if the user was in the database
        //if not goes to twitter api to get it
        if (DatabaseUser is null)
        {
            
            Logger.LogInformation("Getting twitter users...");
            var result = await baseUrl
                .AppendPathSegment("users/lookup.json")
                .WithOAuthBearerToken(TwitterToken)
                .SetQueryParams(new
                {
                    screen_name = userName
                })
                .GetJsonAsync<List<Twitter.Models.TwitterUser>>();
                
                var user = result.FirstOrDefault();

            //if the twitter user exists then this creates a new twitterUser object and adds it to the database
            if (user is not null)
            {
                var insertedUser = await TwitterContext.TwitterUsers
                    .AddAsync(new TwitterUser
                    {
                        ScreenName = user.ScreenName,
                        ExternalId = user.Id
                    });

                await TwitterContext.SaveChangesAsync();
                return insertedUser.Entity;
            }
        }

        return DatabaseUser;
    }
  }
}