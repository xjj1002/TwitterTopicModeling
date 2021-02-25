using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TwitterTopicModeling.Database.Models
{
  public class TwitterUser
  {
    public string ScreenName { get; set; }
    public int Id { get; set; }
    public int ExternalId { get; set; }

    public ICollection<Tweet> Tweets { get; set; }
  }
}