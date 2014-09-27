using Auctionata.Demo.DataAccess.MemoryBased.Repositories;
using Auctionata.Demo.Domain.DataAccess;
using Auctionata.Demo.Domain.Services;
using Auctionata.Demo.Domain.Services.Concrete;

namespace Auctionata.Demo.Application
{
    public class ServiceFactory : IServiceFactory
    {
        // Since we're dealing with in-memory repositories here, we need to keep them available to any service created by
        // a controller per request without creating new repositories each time. 
        // We could use scoping to make the same service available to all requests, thus sharing their repositories, 
        // but for demonstrational purposes, this will suffice.
        private readonly IItemRepository itemRepository;
        private readonly IBidRepository bidRepository;

        public ServiceFactory()
        {
            this.itemRepository = new ItemRepository();
            this.bidRepository = new BidRepository();
        }

        public IItemService CreateItemService()
        {
            return new ItemService(itemRepository, bidRepository);
        }

        public IBidService CreateBidService()
        {
            return new BidService(bidRepository);
        }
    }
}
