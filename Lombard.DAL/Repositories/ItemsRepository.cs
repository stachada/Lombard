using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lombard.BL.Helpers;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Lombard.DAL.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DatabaseContext _context;

        public ItemsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            int addedItemId = item.ItemId;

            return addedItemId;
        }

        public async Task DeleteItemAsync(int itemId)
        {
            var itemToDelete =  await GetItemByIdAsync(itemId);

            if(itemToDelete != null)
            {
                _context.Items.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Item does not exist.");
            }
        }

        public async Task UpdateItemAsync(Item item)
        {
            var itemFromDatabase = await GetItemByIdAsync(item.ItemId);

            if (itemFromDatabase != null)
            {
                _context.Items.Update(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Item does not exist");
            }
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _context.Items.AsNoTracking().FirstOrDefaultAsync(i => i.ItemId == itemId);
        }

        public async Task<IEnumerable<Item>> GetQuantityInCategoriesAsync()
        {
            return await _context.Items.GroupBy(i => i.ProductCategory).Select(x => new Item { ProductCategory = x.Key, Quantity = x.Sum(y => y.Quantity) }).ToListAsync();
        }
    }
}
