using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Employment.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : ApiController
    {
        /// <summary>
        /// Obtiene un objeto anónimo
        /// </summary>
        /// <remarks>Método para testear la disponibilidad de la API.</remarks>
        /// <response code="200">Ok. Objeto devuelto correctamente.</response>
        /// <returns>Datos del objeto anónimo</returns>
        public HttpResponseMessage Get()
        {
            var response = new { Id = 1, Name = "Anthonio" };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
