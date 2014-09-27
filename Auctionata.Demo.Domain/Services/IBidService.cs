using Auctionata.Demo.Domain.Model;
using Auctionata.Demo.Monads;

namespace Auctionata.Demo.Domain.Services
{
    public interface IBidService
    {
        /// <summary>
        ///     Returns the highest bid for an item with the specified id.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Maybe<Bid> GetHighest(string itemId);

        /// <summary>
        ///     Adds a new bid.
        /// </summary>
        /// <param name="bid"></param>
        void Add(BidBase bid);
    }
}
