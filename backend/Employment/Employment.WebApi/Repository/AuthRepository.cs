using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Employment.WebApi.Repository
{
    using Models;
    using Configuration;
    using Dapper;

    public class AuthRepository
    {
        private const string connectionName = "DefaultConnection";
        private string connectionString = DatabaseConfig.ConnectionString(connectionName);

        public async Task<UserInfo> GetUserByUserNameAsync(string userName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserName", userName);
                IEnumerable<UserInfo> user = 
                    await connection.QueryAsync<UserInfo>("dbo.spUsers_GetUserByUserName", 
                                                            p, 
                                                            commandType: CommandType.StoredProcedure);

                return user.FirstOrDefault();
            }
        }

        public async Task<string> GetPasswordHashByUserIdAsync(string userId)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserId", userId);
                IEnumerable<string> passwordHash = 
                    await connection.QueryAsync<string>("dbo.spUsers_GetPasswordHashByUserId",
                                                         p,
                                                         commandType: CommandType.StoredProcedure);
                return passwordHash.FirstOrDefault();
            }
        }
    }
}