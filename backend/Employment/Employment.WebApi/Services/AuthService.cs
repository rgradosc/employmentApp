using System;
using System.Text;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Employment.WebApi.Services
{
    using Models;
    using Repository;

    public class AuthService 
    {
        private readonly AuthRepository authRepository;

        public AuthService()
        {
            authRepository = new AuthRepository();
        }

        public async Task<UserInfo> AuthenticateUserAsync(string userName, string password)
        {
            UserInfo userInfo = await authRepository.GetUserByUserNameAsync(userName);

            if (userInfo != null)
            {
                string passwordHash = await authRepository.GetPasswordHashByUserIdAsync(userInfo.Id.ToString());
                if (Crypto.VerifyHashedPassword(passwordHash, password))
                {
                    return userInfo;
                }

                return null;
            }

            return userInfo;
        }

        public string GenerateTokenJWT(UserInfo userInfo)
        {
            string secretKey = ConfigurationManager.AppSettings["SecretKey"];
            string issuer = ConfigurationManager.AppSettings["Issuer"];
            string audience = ConfigurationManager.AppSettings["Audience"];
            int expires;

            if (!int.TryParse(ConfigurationManager.AppSettings["Expires"], out expires))
            {
                expires = 24;
            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            JwtHeader header = new JwtHeader(signingCredentials);

            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Id.ToString()),
                new Claim("firstName", userInfo.FirstName),
                new Claim("lastName", userInfo.LastName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(ClaimTypes.Role, userInfo.Role)
            };

            JwtPayload payload = new JwtPayload(issuer: issuer,
                                                audience: audience,
                                                claims: claims,
                                                notBefore: DateTime.UtcNow,
                                                expires: DateTime.UtcNow.AddHours(expires));

            JwtSecurityToken token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}