using System.Web.Mvc;

namespace Auctionata.Demo.Application.Rest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
