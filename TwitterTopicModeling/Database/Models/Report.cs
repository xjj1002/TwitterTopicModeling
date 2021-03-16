namespace TwitterTopicModeling.Database.Models
{
    public class Report
    {
        public int id { get; set; }

        public User User { get; set; }

        public string ReportName { get; set; }

        public TwitterUser TwitterUser { get; set; }
    }

}