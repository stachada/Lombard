using Lombard.BL.Helpers;
using Lombard.BL.Models;
using System;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface ITransactionsService
    {
        Task<PagedList<Transaction>> GetTransactions(int pageNumber, int pageSize);
        Task<PagedList<Transaction>> GetTransactionsByCategory(ProductCategory category, int pageNumber, int pageSize);
        Task<PagedList<Transaction>> GetTransactionsToDate(DateTime date, int pageNumber, int pageSize);
        Task<Transaction> BuyAsync(int itemId, int customerId, float quantity, decimal price);
        Task<Transaction> SellAsync(int itemId, int customerId, float quantity, decimal price);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }
}
