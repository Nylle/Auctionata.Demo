using System.Collections.Generic;

namespace Auctionata.Demo.Application.Ui.Models.Home
{
    public class ItemBase
    {
        public string Id { get; set; }

        public IEnumerable<string> ImageLocations { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}