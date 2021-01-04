using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;

namespace Employment.WebApi
{
    using Handler;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Handler validador de tokens
            config.MessageHandlers.Add(new ValidateTokenHandler());

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}
