using Lombard.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
    }
}
