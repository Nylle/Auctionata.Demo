using System.Collections.Generic;
using Auctionata.Demo.Domain.DataAccess;
using Auctionata.Demo.Domain.Model;
using Auctionata.Demo.Monads;

namespace Auctionata.Demo.Domain.Services.Concrete
{
    public class ItemService : IItemService
    {
        private readonly IBidRepository bidRepository;
        private readonly IItemRepository repository;

        public ItemService(IItemRepository repository, IBidRepository bidRepository)
        {
            this.repository = repository;
            this.bidRepository = bidRepository;
        }

        public IEnumerable<Item> Get()
        {
            foreach (var item in repository.Get())
            {
                item.Bids = bidRepository.Get(item.Id);
                yield return item;
            }
        }

        public Maybe<Item> Get(string id)
        {
            if (!repository.Exists(id))
            {
                return Maybe<Item>.Empty;
            }

            var item = repository.Get(id);
            item.Bids = bidRepository.Get(item.Id);
            return new Maybe<Item>(item);
        }
    }
}
