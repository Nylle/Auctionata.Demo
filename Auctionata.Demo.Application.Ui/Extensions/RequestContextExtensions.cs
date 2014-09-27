using System;
using System.Linq;
using System.Web.Routing;

namespace Auctionata.Demo.Application.Ui.Extensions
{
    public static class RequestContextExtensions
    {
        public static bool IsCurrentRoute(this RequestContext context, String controllerName, params String[] actionNames)
        {
            var routeData = context.RouteData;

            return ((String.IsNullOrEmpty(controllerName) || routeData.GetRequiredString("controller") == controllerName)
                    && (actionNames == null || actionNames.Contains(routeData.GetRequiredString("action"))));
        }
    }
}