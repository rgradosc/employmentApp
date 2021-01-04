using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;

namespace Employment.WebApi.Controllers
{
    using Models;

    public class LoginController : ApiController
    {
        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoginAsync(Login login)
        {
            if (login == null)
            {
                return BadRequest("User and password required.");
            }

            UserInfo userInfo = await AutenticarUsuarioAsync(login.UserName, login.Password);

            if (userInfo != null)
            {
                return Ok(new { token = GenerarTokenJWT(userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }

        // COMPROBAMOS SI EL USUARIO EXISTE EN LA BASE DE DATOS 
        private async Task<UserInfo> AutenticarUsuarioAsync(string usuario, string password)
        {
            // TODO: Implementar la lógica de autenticación de usuarios.
            

            // Supondremos que el usuario existe en la Base de Datos.
            // Retornamos un objeto del tipo UserInfo, con toda
            // la información del usuario necesaria para el Token.
            return new UserInfo()
            {
                // Id del Usuario en el Sistema de Información (BD)
                Id = new Guid("B5D233F0-6EC2-4950-8CD7-F44D16EC878F"),
                FirstName = "Raúl",
                LastName = "Grados",
                Email = "grados_2008@hotmail.com",
                Role = "Administrador"
            };

            // Supondremos que el usuario NO existe en la Base de Datos.
            // Retornamos NULL.
            //return null;
        }

        private string GenerarTokenJWT(UserInfo userInfo)
        {
            string claveSecreta = ConfigurationManager.AppSettings["ClaveSecreta"];
            string issuer = ConfigurationManager.AppSettings["Issuer"];
            string audience = ConfigurationManager.AppSettings["Audience"];
            int expires;
            if (!int.TryParse(ConfigurationManager.AppSettings["Expires"], out expires))
            {
                expires = 24;
            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
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

            JwtPayload payload = new JwtPayload(
                                        issuer: issuer,
                                        audience: audience,
                                        claims: claims,
                                        notBefore: DateTime.UtcNow,
                                        // Exipra a la 24 horas.
                                        expires: DateTime.UtcNow.AddHours(expires));
            
            JwtSecurityToken token = new JwtSecurityToken(
                                            header,
                                            payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}