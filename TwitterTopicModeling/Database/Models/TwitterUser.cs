using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


//class and namespace for create a user and save it to the database
namespace TwitterTopicModeling.Database.Models
{
  public class TwitterUser
  {
    public string ScreenName { get; set; }
    public int Id { get; set; }
    public long ExternalId { get; set; }

  }
}