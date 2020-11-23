using System;
using Newtonsoft.Json;

namespace Employment.WebApi.Models
{
    public class Employee
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("dateOfJoining")]
        public DateTime DateOfJoining { get; set; }

        [JsonProperty("photoFileName")]
        public string PhotoFileName { get; set; }
    }
}