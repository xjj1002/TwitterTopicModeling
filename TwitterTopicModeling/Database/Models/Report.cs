namespace TwitterTopicModeling.Database.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    public class Report
    {
        public int id { get; set; }

        public User User { get; set; }

        public string ReportName { get; set; }

        public DateTimeOffset CreatedAt {get; set;} = DateTimeOffset.UtcNow;

        public TwitterUser TwitterUser { get; set; }

        [Column(TypeName = "jsonb")]
        public string data {get; set;}

    }

}