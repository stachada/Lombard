using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombard.DAL.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly DatabaseContext _context;

        public TransactionsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            transaction.SetTransactionDate(DateTime.Now);
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            var transactionFromDb = await GetByIdAsync(transaction.TransactionId);

            if (transactionFromDb != null)
            {
                transaction.SetTransactionDate(DateTime.Now);
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Transaction not found");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var transaction = await GetByIdAsync(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Transaction not found");
            }
        }
    }
}
