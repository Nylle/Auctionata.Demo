using System.Collections.Generic;
using Auctionata.Demo.Domain.Model;

namespace Auctionata.Demo.Domain.DataAccess
{
    public interface IBidRepository
    {
        /// <summary>
        /// Returns all bids for the item with the specified id.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        IEnumerable<Bid> Get(string itemId);

        /// <summary>
        /// Adds a new bid.
        /// </summary>
        /// <param name="bid"></param>
        void Add(BidBase bid);
    }
}