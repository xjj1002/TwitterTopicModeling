using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TwitterTopicModeling.Database.Models;


//This namespace and class are so theat entityFramework can address the database and its tables where it is imported and instaciated
namespace TwitterTopicModeling.Database
{

  public class TwitterContext : DbContext
  {

    public TwitterContext(DbContextOptions<TwitterContext> options) : base(options) { }
    public DbSet<Tweet> Tweets { get; set; }
    public DbSet<TwitterUser> TwitterUsers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Report_tweet> Report_Tweet { get; set; }
    public DbSet<Report> Report { get; set; }



  }
}