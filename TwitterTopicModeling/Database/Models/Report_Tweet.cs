using System.Collections.Generic;

namespace TwitterTopicModeling.Database.Models
{
  public class Report_tweet
  {
    public int id { get; set; }
    public Report Report { get; set; }
    public Tweet Tweet { get; set; }
  }
}