using Lombard.BL.Models;
using LombardAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface IReportService
    {
        Task<decimal> GetTurnover(DateTime start, DateTime end);
        Task<decimal> GetProfit(DateTime start, DateTime end);
        Task<IEnumerable<Item>> GetAllAsync(int pageNumber, int pageSize);
        Task<IEnumerable<CategoryDto>> GetQuantityInCategoriesAsync();
        Task<IEnumerable<Item>> GetItemsWithQuantityLowerThanAsync(float quanity);
    }
}
