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

        public async Task CreateNewItemAsync(Item item)
        {
            if (item.Price <= 0)
                throw new InvalidOperationException("Item price must be positiv.");
            if (item.Name == null || string.IsNullOrEmpty(item.Name))
                throw new InvalidOperationException("Item name can not be empty.");
            if (item.Quantity <= 0)
                throw new InvalidOperationException("Item quantity must be positiv.");

            await _itemsRepository.AddItemAsync(item);
            
        }

        public async Task DeleteItemAsync(int itemId)
        {
            if (itemId <= 0)
                throw new InvalidOperationException("Item Id must be positiv.");

            await _itemsRepository.DeleteItemAsync(itemId);
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _itemsRepository.GetAllAsync();
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            if (itemId <= 0)
                throw new InvalidOperationException("Item id must be positiv.");

            return await _itemsRepository.GetItemByIdAsync(itemId);
        }

        public async Task UpdateItemAsync(Item item)
        {
            if (item.Price <= 0)
                throw new InvalidOperationException("Item price must be positiv.");
            if (item.Name == null || string.IsNullOrEmpty(item.Name))
                throw new InvalidOperationException("Item name can not be empty.");
            if (item.Quantity <= 0)
                throw new InvalidOperationException("Item quantity must be positiv.");

            await _itemsRepository.UpdateItemAsync(item);
        }
    }
}
