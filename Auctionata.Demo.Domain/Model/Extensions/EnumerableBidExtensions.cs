using System.Collections.Generic;
using System.Linq;
using Auctionata.Demo.Monads;

namespace Auctionata.Demo.Domain.Model.Extensions
{
    public static class EnumerableBidExtensions
    {
        /// <summary>
        /// Returns the first highest bid from the enumeration of bids.
        /// </summary>
        /// <param name="bids"></param>
        /// <returns></returns>
        public static Maybe<Bid> FirstHighestBid(this IEnumerable<Bid> bids)
        {
            var enumeratedBids = bids as IList<Bid> ?? bids.ToList();
            return enumeratedBids.Any()
               ? new Maybe<Bid>(enumeratedBids.OrderByDescending(b => b.Amount).ThenBy(b => b.Timestamp).First())
               : Maybe<Bid>.Empty;
        }
    }
}
