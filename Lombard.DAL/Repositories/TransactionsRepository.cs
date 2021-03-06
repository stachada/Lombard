﻿using Lombard.BL.Helpers;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<PagedList<Transaction>> GetTransactions(int pageNumber, int pageSize)
        {
            return await PagedList<Transaction>.CreateAsync(_context.Transactions.OrderBy(t => t.TransactionId), pageNumber, pageSize);
        }

        public async Task<PagedList<Transaction>> GetTransactionsByCategory(ProductCategory category, int pageNumber, int pageSize)
        {
            return await PagedList<Transaction>.CreateAsync(_context.Transactions.Where(t => t.Item.ProductCategory == category), pageNumber, pageSize);
        }

        public async Task<PagedList<Transaction>> GetTransactionsToDate(DateTime date, int pageNumber, int pageSize)
        {
            return await PagedList<Transaction>.CreateAsync(_context.Transactions.Where(t => t.TransactionDate <= date), pageNumber, pageSize);
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
                _context.Remove(transactionFromDb);
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

        public async Task<decimal> GetTurnoverAsync(DateTime start, DateTime end)
        {
            var turnover = await _context.Transactions
                .Where(t => !t.IsPurchase && t.TransactionDate >= start && t.TransactionDate <= end)
                .SumAsync(t => t.GetTransactionAmount());

            return turnover;
        }

        public async Task<decimal> GetProfitAsync(DateTime start, DateTime end)
        {
            var profit = await _context.Transactions
                .Where(t => t.TransactionDate >= start && t.TransactionDate <= end)
                .SumAsync(t => t.GetTransactionAmount());

            return profit;
        }

        public async Task<decimal> GetAveragePriceForItemAsync(int itemId)
        {
            var totalQuantity = await _context.Transactions
                .Where(t => t.Item.ItemId == itemId && t.IsPurchase)
                .SumAsync(t => t.Quantity);
            var totalAmountSpent = await _context.Transactions
                .Where(t => t.Item.ItemId == itemId && t.IsPurchase)
                .SumAsync(t => -t.GetTransactionAmount());

            return totalAmountSpent / (decimal)totalQuantity;
        }
    }
}
