using System.Web.Mvc;

namespace Auctionata.Demo.Application.Rest.Config
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}