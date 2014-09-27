using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Auctionata.Demo.Application.Rest.Models;
using Auctionata.Demo.Domain.Model.Extensions;
using Auctionata.Demo.Domain.Services;

namespace Auctionata.Demo.Application.Rest.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemService itemService;

        public ItemsController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IEnumerable<Item> Get()
        {
            return itemService.Get().Select(Map);
        }

        public Item Get(string id)
        {
            var result = itemService.Get(id);

            if (!result.Any())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Item {0} not found.", id)));
            }

            return Map(result.First());
        }

        private static Item Map(Domain.Model.Item item)
        {
            var highestBid = item.Bids.FirstHighestBid();
            return new Item
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                PictureLocations = item.PictureLocations,
                StartPrice = item.StartPrice,
                HighestBidAmount = highestBid.Any() ? highestBid.First().Amount : (decimal?)null,
                HighestBidderId = highestBid.Any() ? highestBid.First().BidderId : null
            };
        }
    }
}
