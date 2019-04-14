using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lombard.BL.Models;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface IItemsRepository
    {
        Task AddItemAsync(Item item);
        Task DeleteItemAsync(int itemId);
        Task UpdateItemAsync(Item item);
        Task<Item> GetItemByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllAsync();
    }
}
