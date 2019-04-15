using Lombard.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface IItemService 
    {
        Task<int> CreateNewItemAsync(Item item);
        Task DeleteItemAsync(int itemId);
        Task UpdateItemAsync(Item item);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int itemId);
    }
}
