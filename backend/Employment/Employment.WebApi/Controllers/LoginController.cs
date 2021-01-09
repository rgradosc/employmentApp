using System;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using System.Net.Http;

namespace Employment.WebApi.Controllers
{
    using Models;
    using Services;
    using Repository;

    /// <summary>
    /// 
    /// </summary>
    public class LoginController : ApiController
    {
        private readonly AuthService authService;
        private readonly Response response;

        /// <summary>
        /// 
        /// </summary>
        public LoginController()
        {
            authService = new AuthService();
            response = new Response();
        }

        // POST: api/Login
        /// <summary>
        /// Inicia sesión en la API
        /// </summary>
        /// <param name="login">Credenciales de acceso del usuaio.</param>
        /// <response code="200">Ok. Inicia sesión y genera el token JWT.</response>
        /// <response code="400">BadRequest. No se inicio sesión. Formato de los datos incorrectos.</response>
        /// <response code="401">Unauthorized. No se inicio sesión. Usuario no autorizado.</response>
        /// <response code="500">InternalServerError. Error interno del servidor.</response>
        /// <returns>Token de autenticación</returns>
        [HttpPost]
        [AllowAnonymous]
        [ResponseType(typeof(Response))]
        public async Task<HttpResponseMessage> LoginAsync(Login login)
        {
            if (login == null || 
                string.IsNullOrEmpty(login.UserName) || 
                string.IsNullOrEmpty(login.Password))
            {
                response.Message = new Message()
                {
                    Code = "E004",
                    Description = "User and password required.",
                };
                
                return Request.CreateResponse(HttpStatusCode.BadRequest, response); 
            }

            try
            {
                UserInfo userInfo = await authService.AuthenticateUserAsync(login.UserName, login.Password);

                if (userInfo != null)
                {
                    response.Message = new Message()
                    {
                        Code = "A001",
                        Description = "Authorized user.",
                    };

                    response.Data = authService.GenerateTokenJWT(userInfo);

                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    response.Message = new Message()
                    {
                        Code = "E005",
                        Description = "Unauthorized user.",
                    };

                    return Request.CreateResponse(HttpStatusCode.Unauthorized, response);
                }
            }
            catch (Exception ex)
            {
                response.Message = new Message()
                {
                    Code = "E006",
                    Description = "Server error.",
                };

                response.Data = ex.Message;

                return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}