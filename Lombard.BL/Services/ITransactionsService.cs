using Lombard.BL.Models;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface ITransactionsService
    {
        Task<Transaction> BuyAsync(int itemId, int customerId, float quantity, decimal price);
        Task<Transaction> SellAsync(int itemId, int customerId, float quantity, decimal price);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }
}
