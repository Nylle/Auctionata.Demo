using System;

namespace Auctionata.Demo.Domain.Model
{
    public class Bid : BidBase
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
    }
}