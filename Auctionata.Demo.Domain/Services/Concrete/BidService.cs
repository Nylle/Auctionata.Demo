using Auctionata.Demo.Domain.DataAccess;
using Auctionata.Demo.Domain.Model;
using Auctionata.Demo.Domain.Model.Extensions;
using Auctionata.Demo.Monads;

namespace Auctionata.Demo.Domain.Services.Concrete
{
    public class BidService : IBidService
    {
        private readonly IBidRepository repository;

        public BidService(IBidRepository repository)
        {
            this.repository = repository;
        }

        public Maybe<Bid> GetHighest(string itemId)
        {
            return repository.Get(itemId).FirstHighestBid();
        }

        public void Add(BidBase bid)
        {
            repository.Add(bid);
        }
    }
}
