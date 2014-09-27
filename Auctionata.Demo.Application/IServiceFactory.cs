using Auctionata.Demo.Domain.Services;

namespace Auctionata.Demo.Application
{
    public interface IServiceFactory
    {
        IItemService CreateItemService();
        IBidService CreateBidService();
    }
}