using System.Collections.Generic;

namespace Auctionata.Demo.Domain.Model
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> PictureLocations { get; set; }
        public IEnumerable<Bid> Bids { get; set; }
        public decimal StartPrice { get; set; }
    }
}
