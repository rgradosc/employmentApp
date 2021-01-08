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

    public class LoginController : ApiController
    {
        private readonly AuthService authService;
        private readonly Response response;

        public LoginController()
        {
            authService = new AuthService();
            response = new Response();
        }

        // POST: api/Login
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
    }
}