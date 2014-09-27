using System.Collections.Generic;

namespace Auctionata.Demo.Application.Rest.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> PictureLocations { get; set; }
        public decimal? HighestBidAmount { get; set; }
        public string HighestBidderId { get; set; }
        public decimal StartPrice { get; set; }
    }
}
