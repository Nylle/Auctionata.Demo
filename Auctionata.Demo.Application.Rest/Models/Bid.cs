using System;

namespace Auctionata.Demo.Application.Rest.Models
{
    public class Bid : Domain.Model.BidBase
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
    }
}