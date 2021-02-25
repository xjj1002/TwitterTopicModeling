namespace TwitterTopicModeling.Services
{
  using Microsoft.Extensions.Logging;
  using Microsoft.Extensions.Configuration;
  using Flurl;
  using Flurl.Http;
  using TwitterTopicModeling.Twitter.Models;
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;

  public class TwitterService
  {


    public ILogger<TwitterService> Logger { get; }
    public string TwitterToken { get; }
    public string baseUrl = "https://api.twitter.com/1.1";

    public TwitterService(ILogger<TwitterService> logger, IConfiguration configuration)
    {
      Logger = logger;
      TwitterToken = configuration["Twitter:BearerToken"];

    }

    public async Task<List<Tweet>> GetTwitterUserTimeline(TwitterUser twitterUser, int count)
    {


      var result = await baseUrl
      .AppendPathSegment("statuses/user_timeline.json")
      .SetQueryParams(new
      {
        twitterUser.ScreenName,
        count
      })
      .GetJsonAsync<List<Tweet>>();


      Logger.LogInformation("Getting twitter user timeline");

      return result;
    }


    public async Task<TwitterUser> getTwitterUser(string userName)
    {
      Logger.LogInformation("Getting twitter users...");
      var result = await baseUrl
          .AppendPathSegment("users/lookup.json")
          .WithOAuthBearerToken(TwitterToken)
          .SetQueryParams(new
          {
            screen_name = userName
          })
          .GetJsonAsync<List<TwitterUser>>();

      return result.FirstOrDefault();
    }
  }
}