using Newtonsoft.Json;

namespace TwitterTopicModeling.Twitter.Models
{
    public class Tweet
    {
        [JsonProperty("id")]
        public int Id {get; set; }
        
        [JsonProperty("text")]
        public string Text {get; set; }

        [JsonProperty("user")]
        public TwitterUser User {get; set; }
    }
}