using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(int itemId)
        {
            var itemToDelete =  await GetItemById(itemId);

            if(itemToDelete != null)
            {
                _context.Items.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Item does not exist");
            }
        }

        public async Task UpdateItem(Item item)
        {
            var itemFromDatabase = await GetItemById(item.ItemId);

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

        public IEnumerable<Item> GetAll()
        {
            return _context.Items.ToList();
        }

        public async Task<Item> GetItemById(int itemId)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);
        }
    }
}
