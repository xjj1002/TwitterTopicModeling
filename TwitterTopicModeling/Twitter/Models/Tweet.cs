
using System;
using TwitterTopicModeling.Utils;

//https://github.com/JamesNK/Newtonsoft.Json 
//this package is used to assign the attributes in this class to incoming json object values 
using Newtonsoft.Json;

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