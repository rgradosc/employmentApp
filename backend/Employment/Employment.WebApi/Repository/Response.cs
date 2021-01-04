using Newtonsoft.Json;

namespace Employment.WebApi.Repository
{
    public class Response
    {
        [JsonProperty("message")]
        public Message Message { get; set; }
        
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}