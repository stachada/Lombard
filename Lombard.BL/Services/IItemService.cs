using Lombard.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface IItemService 
    {
        Task CreateNewItem(Item item);
        Task DeleteItem(int itemId);
        Task UpdateItem(Item item);
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetItemById(int itemId);
    }
}
