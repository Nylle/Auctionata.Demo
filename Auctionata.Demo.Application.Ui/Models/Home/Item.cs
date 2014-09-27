namespace Auctionata.Demo.Application.Ui.Models.Home
{
    public class Item : ItemBase
    {
        public decimal? HighestBidAmount { get; set; }

        public string HighestBidderId { get; set; }

        public decimal StartPrice { get; set; }
    }
}