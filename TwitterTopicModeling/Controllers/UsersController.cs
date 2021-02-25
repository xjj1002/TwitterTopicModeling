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
  using TwitterTopicModeling.Database.Models;
  using TwitterTopicModeling.Database;
  using Microsoft.EntityFrameworkCore;

  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    public ILogger<TwitterUsersController> Logger { get; }

    public TwitterContext TwitterContext;



    public UsersController(ILogger<TwitterUsersController> logger, TwitterContext twitterContext)
    {

      Logger = logger;
      TwitterContext = twitterContext;


    }


    public async Task<User> getUser(string UserName)
    {

      var DatabaseUser = await TwitterContext.Users
     .Where(x => x.userName == UserName)
     .FirstOrDefaultAsync();

      return DatabaseUser;

    }
  }
}