using Lombard.BL.Helpers;
using Lombard.BL.Models;
using System;
using System.Threading.Tasks;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ITransactionsRepository
    {
        Task<PagedList<Transaction>> GetTransactions(int pageNumber, int pageSize);
        Task<PagedList<Transaction>> GetTransactionsByCategory(ProductCategory category, int pageNumber, int pageSize);
        Task<PagedList<Transaction>> GetTransactionsToDate(DateTime date, int pageNumber, int pageSize);
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<decimal> GetTurnoverAsync(DateTime start, DateTime end);
        Task<decimal> GetProfitAsync(DateTime start, DateTime end);
        Task<decimal> GetAveragePriceForItemAsync(int itemId);
    }
}
