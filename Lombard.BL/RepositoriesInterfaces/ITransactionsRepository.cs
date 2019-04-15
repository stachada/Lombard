using Lombard.BL.Helpers;
using Lombard.BL.Models;
using System;
using System.Threading.Tasks;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ITransactionsRepository
    {
        Task<PagedList<Transaction>> GetTransactions(int pageNumber, int pageSize);
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<decimal> GetTurnover(DateTime start, DateTime end);
        Task<decimal> GetProfit(DateTime start, DateTime end);
    }
}
