using System;
using System.Collections.Generic;
using System.Linq;
using Auctionata.Demo.Domain.DataAccess;
using Auctionata.Demo.Domain.Model;

namespace Auctionata.Demo.DataAccess.MemoryBased.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IEnumerable<Item> items;

        public ItemRepository()
        {
            items = new List<Item>
                {
                    new Item
                        {
                            Name = "1965 Citroën DS 19",
                            Description = "This beautiful car, usually called \"La Déesse\" (the goddess) and formerly owned by the French villain Fantomas, has undergone a heavy conversion adding foldable wings and a retractable jet engine in the aft section.",
                            Id = "1",
                            PictureLocations = new[] {"http://i.imgur.com/U6p3vat.png"},
                            StartPrice = 1500,
                            Bids = Enumerable.Empty<Bid>(),
                        },
                    new Item
                        {
                            Name = "1974 Dodge Monaco",
                            Description = "This Mount Prospect police car's got a cop motor, a 440-cubic-inch plant. It's got cop tires, cop suspension, cop shocks. It's a model made before catalytic converters so it'll run good on regular gas.",
                            Id = "2",
                            PictureLocations = new[] {"http://i.imgur.com/68YLolk.gif"},
                            StartPrice = 500,
                            Bids = Enumerable.Empty<Bid>(),
                        },
                    new Item
                        {
                            Name = "Emmy Award",
                            Description = "This Emmy once belonged to Evan Greer for playing Dr. Brock Sterling on the television soap opera \"Prescription Passion\".",
                            Id = "3",
                            PictureLocations = new[] {"http://i.imgur.com/sxzEhoB.gif"},
                            StartPrice = 50,
                            Bids = Enumerable.Empty<Bid>(),
                        }
                };
        }

        public IEnumerable<Item> Get()
        {
            return items;
        }

        public bool Exists(string id)
        {
            return items.Any(i => i.Id == id);
        }

        public Item Get(string id)
        {
            return items.Single(i => i.Id == id);
        }
    }
}
