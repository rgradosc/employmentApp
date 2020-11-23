using Newtonsoft.Json;

namespace Employment.WebApi.Repository
{
    public class MessageOk
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}