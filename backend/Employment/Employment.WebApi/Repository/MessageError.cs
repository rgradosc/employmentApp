using Newtonsoft.Json;

namespace Employment.WebApi.Repository
{
    public class MessageError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}