using System;
using System.Threading.Tasks;
using Lombard.BL.Helpers;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;

namespace Lombard.BL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepo;
        private readonly IItemsRepository _itemsRepo;
        private readonly ICustomersRepository _customersRepo;

        public TransactionsService(ITransactionsRepository transactionsRepo, IItemsRepository itemsRepo, ICustomersRepository customersRepo)
        {
            _transactionsRepo = transactionsRepo;
            _itemsRepo = itemsRepo;
            _customersRepo = customersRepo;
        }

        public async Task<PagedList<Transaction>> GetTransactions(int pageNumber, int pageSize)
        {
            return await _transactionsRepo.GetTransactions(pageNumber, pageSize);
        }

        public async Task<PagedList<Transaction>> GetTransactionsByCategory(ProductCategory category, int pageNumber, int pageSize)
        {
            return await _transactionsRepo.GetTransactionsByCategory(category, pageNumber, pageSize);
        }

        public async Task<PagedList<Transaction>> GetTransactionsToDate(DateTime date, int pageNumber, int pageSize)
        {
            return await _transactionsRepo.GetTransactionsToDate(date, pageSize, pageSize);
        }

        public async Task<Transaction> BuyAsync(int itemId, int customerId, float quantity, decimal price)
        {
            var item = await _itemsRepo.GetItemByIdAsync(itemId);

            if (item == null) throw new InvalidOperationException("Item not found");

            var customer = await _customersRepo.GetCustomerByIdAsync(customerId);

            if (quantity <= 0) throw new InvalidOperationException("Quantity must be positive");

            if (price <= 0) throw new InvalidOperationException("Price must be positive");

            item.IncreaseItemQuantityByGivenValue(quantity);
            await _itemsRepo.UpdateItemAsync(item);
            var transaction = Transaction.CreateTransaction(item, customer, quantity, price);
            await _transactionsRepo.AddAsync(transaction);
            var averagePrice = await _transactionsRepo.GetAveragePriceForItemAsync(item.ItemId);
            item.Price = averagePrice;
            await _itemsRepo.UpdateItemAsync(item);
            return transaction;
        }

        public async Task<Transaction> SellAsync(int itemId, int customerId, float quantity, decimal price)
        {
            var item = await _itemsRepo.GetItemByIdAsync(itemId);

            if (item == null) throw new InvalidOperationException("Item not found");

            var customer = await _customersRepo.GetCustomerByIdAsync(customerId);

            if (quantity <= 0) throw new InvalidOperationException("Quantity must be positive");

            if (price <= 0) throw new InvalidOperationException("Price must be positive");

            item.DecreaseItemQuantityByGivenValue(quantity);

            if (item.Quantity < 0) throw new InvalidOperationException("There is no enough items in stock");

            await _itemsRepo.UpdateItemAsync(item);
            var transaction = Transaction.CreateTransaction(item, customer, -quantity, price);
            await _transactionsRepo.AddAsync(transaction);
            return transaction;
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            if (transaction.Price <= 0) throw new InvalidOperationException("Price must be positive");

            await _transactionsRepo.UpdateAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionsRepo.DeleteAsync(id);
        }

        public async Task<decimal> GetTurnoverAsync(DateTime start, DateTime end)
        {
            if (start > end) throw new InvalidOperationException("Start date must be before End date");

            return await _transactionsRepo.GetTurnoverAsync(start, end);
        }

        public async Task<decimal> GetProfitAsync(DateTime start, DateTime end)
        {
            if (start > end) throw new InvalidOperationException("Start date must be before End date");

            return await _transactionsRepo.GetProfitAsync(start, end);
        }
    }
}
