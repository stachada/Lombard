using System;
using System.Collections.Generic;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;

namespace Lombard.DAL.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private readonly DatabaseContext _context;

        public ItemsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(int itemId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAll()
        {
            throw new NotImplementedException();
        }

        public Item GetItemById(int itemId)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
