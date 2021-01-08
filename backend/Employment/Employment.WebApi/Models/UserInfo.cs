using System;

namespace Employment.WebApi.Models
{
    public class UserInfo
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public UserInfo()
        {
            Role = string.Empty;
        }
    }
}