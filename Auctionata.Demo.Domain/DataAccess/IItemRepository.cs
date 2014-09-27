using System.Collections.Generic;
using Auctionata.Demo.Domain.Model;

namespace Auctionata.Demo.Domain.DataAccess
{
    public interface IItemRepository
    {
        /// <summary>
        /// Returns a collection of items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Item> Get();

        /// <summary>
        /// Returns the item with the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Item Get(string id);

        /// <summary>
        /// Returns whether an item with the specified id exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(string id);
    }
}