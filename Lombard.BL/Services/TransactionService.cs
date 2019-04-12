using System;
using System.Threading.Tasks;
using Lombard.BL.RepositoriesInterfaces;

namespace Lombard.BL.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionsRepo;
        private readonly IItemsRepository _itemsRepo;
        // private readonly ICustomersRepository _customersRepo;

        public TransactionService(ITransactionsRepository transactionsRepo, IItemsRepository itemsRepo)
        {
            _transactionsRepo = transactionsRepo;
            _itemsRepo = itemsRepo;
        }

        public async Task BuyAsync(int itemId, int customerId, int quantity, decimal price)
        {
            // Get item from database
            // Get customer from database
            // Validate transaction
            // Update quantity on item
            // Create Transaction
            // Save Item
            // Save Transaction
            throw new NotImplementedException();
        }

        public async Task SellAsync(int itemId, int customerId, int quantity, decimal price)
        {
            // Get item from database
            // Get customer from database
            // Validate transaction
            // Update quantity on item
            // Create Transaction
            // Save Item
            // Save Transaction
            throw new NotImplementedException();
        }
    }
}
