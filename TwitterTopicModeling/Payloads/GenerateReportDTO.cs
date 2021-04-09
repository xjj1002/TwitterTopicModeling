namespace TwitterTopicModeling.Payloads
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    using CsvHelper.Configuration.Attributes;
    using System.Collections.Generic;
    using Database.Models;
    public class GenerateReportDTO
    {

        public int id { get; set; }

        public User User { get; set; }

        public string ReportName { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public TwitterUser TwitterUser { get; set; }

        public IEnumerable<ReportTopic> Topics { get; set; }

        public List<Tweet> ReportTweets { get; set; }
    }
}