namespace TwitterTopicModeling.Database.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    using CsvHelper.Configuration.Attributes;
    using System.Collections.Generic;
    public class Report
    {
        public int id { get; set; }

        public User User { get; set; }

        public string ReportName { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public Boolean malFlag {get; set;} = false;

        public TwitterUser TwitterUser { get; set; }

        [Column(TypeName = "jsonb")]
        public IEnumerable<ReportTopic> Topics { get; set; }

        public List<Report_tweet> ReportTweets {get; set;} = new List<Report_tweet>();

    }


    public class ReportTopic
    {
        [Index(0)]
        public string Topic { get; set; }

        [Index(1)]
        public int Count { get; set; }

    }
}