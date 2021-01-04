using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Employment.WebApi.Handler
{
    internal class ValidateTokenHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var claveSecreta = ConfigurationManager.AppSettings["ClaveSecreta"];
                var issuerToken = ConfigurationManager.AppSettings["Issuer"];
                var audienceToken = ConfigurationManager.AppSettings["Audience"];

                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(claveSecreta));

                SecurityToken securityToken;
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    // DELEGADO PERSONALIZADO PERA COMPROBAR
                    // LA CADUCIDAD EL TOKEN.
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                // COMPRUEBA LA VALIDEZ DEL TOKEN
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token,
                                                                     validationParameters,
                                                                     out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token,
                                                                      validationParameters,
                                                                      out securityToken);

                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            return Task<HttpResponseMessage>.Factory.StartNew(() =>
                        new HttpResponseMessage(statusCode) { });
        }

        // RECUPERA EL TOKEN DE LA PETICIÓN
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) ||
                                              authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ?
                    bearerToken.Substring(7) : bearerToken;
            return true;
        }

        // COMPRUEBA LA CADUCIDAD DEL TOKEN
        public bool LifetimeValidator(DateTime? notBefore,
                                      DateTime? expires,
                                      SecurityToken securityToken,
                                      TokenValidationParameters validationParameters)
        {
            bool valid = false;

            if ((expires.HasValue && DateTime.UtcNow < expires)
                && (notBefore.HasValue && DateTime.UtcNow > notBefore))
            {
                valid = true;
            }

            return valid;
        }
    }
}