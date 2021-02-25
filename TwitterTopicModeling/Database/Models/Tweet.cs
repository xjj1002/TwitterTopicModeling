using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TwitterTopicModeling.Database.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public long ExternalId { get; set; }
        public string Text { get; set; }
        public TwitterUser TwitterUser { get; set; }

    }
}