using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Auctionata.Demo.Application.Rest.Controllers;

namespace Auctionata.Demo.Application.Rest.Ioc
{
    public class PoorMansCompositionRoot : IHttpControllerActivator
    {
        // This code is lifted from Mark Seemann's excellent blog: http://blog.ploeh.dk/2012/09/28/DependencyInjectionandLifetimeManagementwithASP.NETWebAPI/
        // In his book "Dependency Injection in .NET" he coined the term "Poor Man's DI" for manually composing the dependency graph instead of using
        // a container like Unity or Castle.
        // I would strongly recommend using a container in a production environment, while it would exceed the available time on this demo.

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var factory = new ServiceFactory();

            if (controllerType == typeof (ItemsController))
            {
                return new ItemsController(factory.CreateItemService());
            }

            if (controllerType == typeof (BidsController))
            {
                return new BidsController(factory.CreateBidService());
            }

            return null;
        }
    }
}
