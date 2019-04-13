using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;

namespace Lombard.BL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemService(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public async Task CreateNewItem(Item item)
        {
            if (item.Price <= 0)
                throw new InvalidOperationException("Item price must be positiv.");
            if (item.Name == null)
                throw new InvalidOperationException("Item name can not be empty.");
            if (item.Quantity <= 0)
                throw new InvalidOperationException("Item quantity must be positiv.");

            await _itemsRepository.AddItem(item);
            
        }

        public async Task DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public async Task<Item> GetItemById(int itemId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
