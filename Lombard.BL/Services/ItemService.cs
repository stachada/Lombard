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
            if (item.Name == null || string.IsNullOrEmpty(item.Name))
                throw new InvalidOperationException("Item name can not be empty.");
            if (item.Quantity <= 0)
                throw new InvalidOperationException("Item quantity must be positiv.");

            await _itemsRepository.AddItem(item);
            
        }

        public async Task DeleteItem(int itemId)
        {
            if (itemId <= 0)
                throw new InvalidOperationException("Item Id must be positiv.");

            await _itemsRepository.DeleteItem(itemId);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemsRepository.GetAll();
        }

        public async Task<Item> GetItemById(int itemId)
        {
            if (itemId <= 0)
                throw new InvalidOperationException("Item id must be positiv.");

            return await _itemsRepository.GetItemById(itemId);
        }

        public async Task UpdateItem(Item item)
        {
            if (item.Price <= 0)
                throw new InvalidOperationException("Item price must be positiv.");
            if (item.Name == null || string.IsNullOrEmpty(item.Name))
                throw new InvalidOperationException("Item name can not be empty.");
            if (item.Quantity <= 0)
                throw new InvalidOperationException("Item quantity must be positiv.");

            await _itemsRepository.UpdateItem(item);
        }
    }
}
