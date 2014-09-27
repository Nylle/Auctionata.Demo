namespace Auctionata.Demo.Domain.Model
{
    public class BidBase
    {
        public string ItemId { get; set; }
        public string BidderId { get; set; }
        public decimal Amount { get; set; }
    }
}
