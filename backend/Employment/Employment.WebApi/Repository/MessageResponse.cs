using Newtonsoft.Json;

namespace Employment.WebApi.Repository
{
    public class MessageResponse
    {
        [JsonProperty("messageOk")]
        public MessageOk MessageOk { get; set; }

        [JsonProperty("messageError")]
        public MessageError MessageError { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}