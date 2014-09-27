using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Auctionata.Demo.Application.Rest.Models;
using Auctionata.Demo.Domain.Services;

namespace Auctionata.Demo.Application.Rest.Controllers
{
    public class BidsController : ApiController
    {
        private readonly IBidService bidService;

        public BidsController(IBidService bidService)
        {
            this.bidService = bidService;
        }

        public HttpResponseMessage Put([FromBody] BidBase value)
        {
            try
            {
                bidService.Add(Map(value));
            }
            catch (Exception e)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, String.Format("Error adding bid for item {0}.", value.ItemId)));
            }

            return Request.CreateResponse(HttpStatusCode.OK, "success");
        }

        public Bid Get(string itemId)
        {
            var result = bidService.GetHighest(itemId);
            if (!result.Any())
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("No bid for item {0} found.", itemId)));
            }

            return Map(result.First());
        }

        private static Domain.Model.BidBase Map(BidBase bid)
        {
            return new Domain.Model.BidBase
                {
                    Amount = bid.Amount,
                    BidderId = bid.BidderId,
                    ItemId = bid.ItemId
                };
        }

        private static Bid Map(Domain.Model.Bid bid)
        {
            return new Bid
                {
                    Amount = bid.Amount,
                    BidderId = bid.BidderId,
                    Id = bid.Id,
                    ItemId = bid.ItemId,
                    Timestamp = bid.Timestamp
                };
        }
    }
}
