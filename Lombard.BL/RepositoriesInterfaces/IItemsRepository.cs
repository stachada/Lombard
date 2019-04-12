using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lombard.BL.Models;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface IItemsRepository
    {
        Task AddItem(Item item);
        Task DeleteItem(int itemId);
        Task UpdateItem(Item item);
        Task<Item> GetItemById(int itemId);
        IEnumerable<Item> GetAll();
    }
}
