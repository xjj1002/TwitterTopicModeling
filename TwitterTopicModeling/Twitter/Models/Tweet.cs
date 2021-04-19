using Newtonsoft.Json;
using System;
using TwitterTopicModeling.Utils;

namespace TwitterTopicModeling.Twitter.Models
{
    public class Tweet
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("user")]
        public TwitterUser User { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt {get; set;}
    }
}