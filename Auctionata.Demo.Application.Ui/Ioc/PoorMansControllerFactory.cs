using System.Web.Mvc;
using System.Web.Routing;
using Auctionata.Demo.Application.Ui.Controllers;

namespace Auctionata.Demo.Application.Ui.Ioc
{
    public class PoorMansControllerFactory : DefaultControllerFactory
    {
        private readonly IServiceFactory factory = new ServiceFactory();

        // This code is lifted from Mark Seemann's book "Dependency Injection in .NET". 
        // He coined the term "Poor Man's DI" for manually composing the dependency graph instead of using a container like Unity or Castle.
        // I would strongly recommend using a container in a production environment, while it would exceed the available time on this demo.

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName.ToLower())
            {
                case "home":
                    return new HomeController(factory.CreateItemService(), factory.CreateBidService());
                default:
                    return base.CreateController(requestContext, controllerName);
            }
        }
    }
}
