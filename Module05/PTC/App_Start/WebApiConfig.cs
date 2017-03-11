using System.Web.Http;

namespace PTC
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config) {
            config.MapHttpAttributeRoutes();

            // Route for API Calls
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}