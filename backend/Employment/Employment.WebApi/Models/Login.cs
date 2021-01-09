using Newtonsoft.Json;

namespace Employment.WebApi.Models
{
    public class Login
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}