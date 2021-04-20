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
    using Microsoft.EntityFrameworkCore;

    //my classes
    using TwitterTopicModeling.Database.Models;
    using TwitterTopicModeling.Database;
    using TwitterTopicModeling.Payloads;

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


        //testing method used early on to just get a user that had been saved to the database by username
        [HttpGet("getUser/{username}")]
        public async Task<User> getUser(string UserName)
        {

            var DatabaseUser = await TwitterContext.Users
           .Where(x => x.userName == UserName)
           .FirstOrDefaultAsync();

            return DatabaseUser;

        }

        //developer endpoint used to create a new user in the system cannot be used within application yet
        [HttpPost("createUser")]
        public async Task<User> createUser(UserDTO user)
        {

            var insertedUser = await TwitterContext.Users
                .AddAsync(new User
                {
                    userName = user.username,
                    password = user.password
                });

            await TwitterContext.SaveChangesAsync();
            return insertedUser.Entity;
        }

        //endpoint useed to verify the username and password that is being entered into the system
        //it returns a IActionsResult object which gives us the ability to repurt a unauthroized message as the httpp reponse
        //if it is valid it returns ok and the user that matched
        [HttpPost("login")]
        public async Task<IActionResult> login(UserDTO user)
        {

            var rtnUser = await TwitterContext.Users
                .FirstOrDefaultAsync(x => 
                    x.userName == user.username && 
                    x.password == user.password
                );

            if(rtnUser is null)
            {
                return Unauthorized();
            }

            return Ok(rtnUser);

        }
    }
}