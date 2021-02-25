using Newtonsoft.Json;

namespace TwitterTopicModeling.Twitter.Models
{
    public class TwitterUser
    {
        [JsonProperty("id")]
        public long Id {get; set; }
        
        [JsonProperty("screen_name")]
        public string ScreenName {get; set; }


    }
}