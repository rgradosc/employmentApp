using System.Configuration;

namespace Employment.WebApi.Configuration
{
    public static class DatabaseConfig
    {
        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}