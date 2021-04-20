using System.Collections.Generic;


//databse model for connecting the tweets and the reports. 
//makes it so you can address all the aspects inside a report or tweet later on 
namespace TwitterTopicModeling.Database.Models
{
  public class Report_tweet
  {
    public int id { get; set; }
    public Report Report { get; set; }
    public Tweet Tweet { get; set; }
    public bool flag {get; set;}
  }
}