using System.IO;
using System.Web.Http;
using WebActivatorEx;
using Employment.WebApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Employment.WebApi
{
    public class SwaggerConfig
    {
        protected static string GetXmlCommentsPath()
        {
            return Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "bin", "Employment.WebApi.xml");
        }

        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(config =>
                {
                    config.SingleApiVersion("v1", "Employment API")
                        .Description("API para administrar la información de empleados y departamentos.")
                        .Contact(x => x
                                .Name("Raúl Grados")
                                .Url("https://github.com/rgradosc")
                                .Email("gnr2092@gmail.com"))
                        .License(x => x
                                .Name("Licencia")
                                .Url("https://github.com/rgradosc/employmentApp/blob/main/LICENSE"));

                    config.PrettyPrint();

                    config.ApiKey("Authorization")
                        .Description("JWT Authentication")
                        .Name("Bearer")
                        .In("header");

                    config.IncludeXmlComments(GetXmlCommentsPath());

                })
                .EnableSwaggerUi(config =>
                {
                    config.EnableApiKeySupport("Authorization", "header");
                });
        }
    }
}