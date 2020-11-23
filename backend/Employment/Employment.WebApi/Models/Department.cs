using Newtonsoft.Json;

namespace Employment.WebApi.Models
{
    public class Department
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}