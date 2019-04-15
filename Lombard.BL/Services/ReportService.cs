using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using LombardAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public class ReportService : IReportService
    {
        private readonly ITransactionsRepository _transactionsRepo;
        private readonly IItemsRepository _itemsRepos;

        public ReportService(ITransactionsRepository transactionsRepo, IItemsRepository itemsRepos)
        {
            _transactionsRepo = transactionsRepo;
            _itemsRepos = itemsRepos;
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            return await _itemsRepos.GetAllAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsWithQuantityLowerThanAsync(float quanity)
        {
            return await _itemsRepos.GetItemsWithQuantityLowerThanAsync(quanity);
        }

        public async Task<decimal> GetProfit(DateTime start, DateTime end)
        {
            return await _transactionsRepo.GetProfit(start, end);
        }

        public async Task<IEnumerable<CategoryDto>> GetQuantityInCategoriesAsync()
        {
            return await _itemsRepos.GetQuantityInCategoriesAsync();
        }

        public async Task<decimal> GetTurnover(DateTime start, DateTime end)
        {
            return await _transactionsRepo.GetTurnover(start, end);
        }
    }
}
