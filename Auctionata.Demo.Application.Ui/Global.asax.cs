using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Auctionata.Demo.Application.Ui.Config;
using Auctionata.Demo.Application.Ui.Ioc;

namespace Auctionata.Demo.Application.Ui
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // We use our own IControllerFactory for proper DI.
            ControllerBuilder.Current.SetControllerFactory(new PoorMansControllerFactory());

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
