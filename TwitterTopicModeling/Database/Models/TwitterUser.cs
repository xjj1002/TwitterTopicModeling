using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TwitterTopicModeling.Database.Models
{
  public class TwitterUser
  {
    public string ScreenName { get; set; }
    public int Id { get; set; }
    public long ExternalId { get; set; }

  }
}