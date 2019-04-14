using Lombard.BL.RepositoriesInterfaces;
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

        public async Task<decimal> GetProfit(DateTime start, DateTime end)
        {
            return await _transactionsRepo.GetProfit(start, end);
        }

        public async Task<decimal> GetTurnover(DateTime start, DateTime end)
        {
            return await _transactionsRepo.GetTurnover(start, end);
        }
    }
}
