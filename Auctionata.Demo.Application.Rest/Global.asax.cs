using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using Auctionata.Demo.Application.Rest.Config;
using Auctionata.Demo.Application.Rest.Ioc;

namespace Auctionata.Demo.Application.Rest
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // We use our own IHttpControllerActivator for proper DI.
            GlobalConfiguration.Configuration.Services.Replace(typeof (IHttpControllerActivator), new PoorMansCompositionRoot());
            
            // We only want JSON
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
