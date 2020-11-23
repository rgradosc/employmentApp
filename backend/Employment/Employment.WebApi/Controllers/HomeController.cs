using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Employment.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var response = new { Id = 1, Name = "Anthonio" };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
