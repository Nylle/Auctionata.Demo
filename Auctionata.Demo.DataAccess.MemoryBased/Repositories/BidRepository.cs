using System;
using System.Collections.Generic;
using System.Linq;
using Auctionata.Demo.Domain.DataAccess;
using Auctionata.Demo.Domain.Model;

namespace Auctionata.Demo.DataAccess.MemoryBased.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly IList<Bid> bids;

        public BidRepository()
        {
            bids = new List<Bid>
                {
                    new Bid
                        {
                            Id = Guid.NewGuid().ToString(),
                            ItemId = "1",
                            Amount = 2000,
                            BidderId = "A",
                            Timestamp = DateTime.Now.AddMinutes(-5)
                        },
                    new Bid
                        {
                            Id = Guid.NewGuid().ToString(),
                            ItemId = "1",
                            Amount = 2000,
                            BidderId = "B",
                            Timestamp = DateTime.Now.AddMinutes(-4)
                        },
                    new Bid
                        {
                            Id = Guid.NewGuid().ToString(),
                            ItemId = "2",
                            Amount = 550,
                            BidderId = "C",
                            Timestamp = DateTime.Now.AddMinutes(-1)
                        }
                };
        }

        public IEnumerable<Bid> Get(string itemId)
        {
            return bids.Where(b => b.ItemId == itemId);
        }

        public void Add(BidBase bid)
        {
            var result = new Bid
                {
                    Amount = bid.Amount,
                    BidderId = bid.BidderId,
                    ItemId = bid.ItemId, 
                    Id = Guid.NewGuid().ToString(), 
                    Timestamp = DateTime.Now
                };

            bids.Add(result);
        }
    }
}
