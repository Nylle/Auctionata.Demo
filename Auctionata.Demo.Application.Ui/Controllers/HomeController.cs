using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auctionata.Demo.Application.Ui.Models.Home;
using Auctionata.Demo.Domain.Model;
using Auctionata.Demo.Domain.Model.Extensions;
using Auctionata.Demo.Domain.Services;
using Bid = Auctionata.Demo.Application.Ui.Models.Home.Bid;
using Item = Auctionata.Demo.Application.Ui.Models.Home.Item;

namespace Auctionata.Demo.Application.Ui.Controllers
{
    public class HomeController : Controller
    {
        private readonly IItemService itemService;
        private readonly IBidService bidService;

        public HomeController(IItemService itemService, IBidService bidService)
        {
            this.itemService = itemService;
            this.bidService = bidService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Items()
        {
            var model = itemService.Get().Select(MapItemBase);

            return View(model);
        }

        public ActionResult Item(string id)
        {
            var model = itemService.Get(id);

            if (!model.Any())
            {
                throw new HttpException(404, string.Format("The item with the id '{0}' was not found.", id));
            }

            return View(MapItem(model.First()));
        }

        public ActionResult MakeBid(string id, decimal bidAmount, string bidderId)
        {
            // I skipped validation due to time constraints. 
            // Of course you should always validate on server-side.
            bidService.Add(new BidBase
                {
                    Amount = bidAmount,
                    BidderId = bidderId,
                    ItemId = id
                });
            return GetHighestBid(id, bidderId);
        }

        public JsonResult GetHighestBid(string id, string username)
        {
            var result = bidService.GetHighest(id);
            if (result.Any())
            {
                var bid = new Bid
                    {
                        Amount = result.First().Amount, 
                        IsForeignBidder = result.First().BidderId != username
                    };
                return Json(bid, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        private static ItemBase MapItemBase(Domain.Model.Item item)
        {
            return new ItemBase
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ImageLocations = item.PictureLocations
            };
        }

        private static Item MapItem(Domain.Model.Item item)
        {
            var highestBid = item.Bids.FirstHighestBid();

            return new Item
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ImageLocations = item.PictureLocations,
                HighestBidAmount = highestBid.Any() ? highestBid.First().Amount : 0,
                HighestBidderId = highestBid.Any() ? highestBid.First().BidderId : null,
                StartPrice = item.StartPrice
            };
        }
    }
}
