using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lombard.BL.Helpers;
using Lombard.BL.Models;
using LombardAPI.Dtos;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface IItemsRepository
    {
        Task<int> AddItemAsync(Item item);
        Task DeleteItemAsync(int itemId);
        Task UpdateItemAsync(Item item);
        Task<Item> GetItemByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllAsync();
        Task<IEnumerable<CategoryDto>> GetQuantityInCategoriesAsync();
        Task<IEnumerable<Item>> GetItemsWithQuantityLowerThanAsync(float quanity);
    }
}
