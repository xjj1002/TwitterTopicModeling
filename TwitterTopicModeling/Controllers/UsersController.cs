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

        [HttpGet("getUser/{username}")]
        public async Task<User> getUser(string UserName)
        {

            var DatabaseUser = await TwitterContext.Users
           .Where(x => x.userName == UserName)
           .FirstOrDefaultAsync();

            return DatabaseUser;

        }

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


        [HttpPost("login")]
        public async Task<bool> login(UserDTO user)
        {

            return await TwitterContext.Users
                .AnyAsync(x => 
                    x.userName == user.username && 
                    x.password == user.password
                );

        }
    }
}