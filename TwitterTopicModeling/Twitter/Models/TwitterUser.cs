
//https://github.com/JamesNK/Newtonsoft.Json 
//this package is used to assign the attributes in this class to incoming json object values
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