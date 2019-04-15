using Lombard.BL.Helpers;
using Lombard.BL.Models;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface IItemService 
    {
        Task<int> CreateNewItemAsync(Item item);
        Task DeleteItemAsync(int itemId);
        Task UpdateItemAsync(Item item);
        Task<PagedList<Item>> GetAllItemsAsync(int pageNumber, int pageSize);
        Task<Item> GetItemByIdAsync(int itemId);
    }
}
