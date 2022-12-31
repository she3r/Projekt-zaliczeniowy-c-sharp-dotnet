using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektZaliczeniowy.Entities;

namespace ProjektZaliczeniowy.Repositories
{


    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item() { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item() { Id = Guid.NewGuid(), Name = "Bronze shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }
        public async Task<Item> GetItemAsync(Guid id)
        {
            return await Task.FromResult(items.Where(item => item.Id == id).SingleOrDefault());
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
            await Task.CompletedTask;   // create task that already completed and return it
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(exItem => exItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(exItem => exItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}