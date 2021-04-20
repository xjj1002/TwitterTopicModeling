namespace TwitterTopicModeling.Database.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    using CsvHelper.Configuration.Attributes;
    using System.Collections.Generic;

    //this class is the main class for the application
    //once all the data is gathered this class will be instaciated to save to the database
    public class Report
    {
        public int id { get; set; }

        public User User { get; set; }

        public string ReportName { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public Boolean malFlag {get; set;} = false;

        public TwitterUser TwitterUser { get; set; }


        //this jsonb is used to save this list of objects as json in the database
        [Column(TypeName = "jsonb")]
        public IEnumerable<ReportTopic> Topics { get; set; }

        public List<Report_tweet> ReportTweets {get; set;} = new List<Report_tweet>();

    }

    //this is so that the report has an object for the topics that are being saved when it is generated
    //will be used in the report class above
    public class ReportTopic
    {
        [Index(0)]
        public string Topic { get; set; }

        [Index(1)]
        public int Count { get; set; }

    }
}