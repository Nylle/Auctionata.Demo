using System.Collections.Generic;
using Auctionata.Demo.Domain.Model;
using Auctionata.Demo.Monads;

namespace Auctionata.Demo.Domain.Services
{
    public interface IItemService
    {
        /// <summary>
        /// Returns all items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Item> Get();
        
        /// <summary>
        /// Returns the item with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Maybe<Item> Get(string id);
    }
}
