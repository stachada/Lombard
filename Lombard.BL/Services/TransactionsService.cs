﻿using System;
using System.Threading.Tasks;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;

namespace Lombard.BL.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepo;
        private readonly IItemsRepository _itemsRepo;
        //private readonly ICustomersRepository _customersRepo;

        public TransactionsService(ITransactionsRepository transactionsRepo, IItemsRepository itemsRepo)
        {
            _transactionsRepo = transactionsRepo;
            _itemsRepo = itemsRepo;
        }

        public async Task BuyAsync(int itemId, int customerId, int quantity, decimal price)
        {
            var item = await _itemsRepo.GetItemById(itemId);

            if (item == null) throw new InvalidOperationException("Item not found");

            if (quantity <= 0) throw new InvalidOperationException("Quantity must be positive");

            if (price <= 0) throw new InvalidOperationException("Price must be positive");

            item.IncreaseItemQuantityByGivenValue(quantity);
            await _itemsRepo.UpdateItem(item);
            var transaction = Transaction.CreateTransaction(item, new Customer(), quantity, price);
            await _transactionsRepo.AddAsync(transaction);

        }

        public async Task SellAsync(int itemId, int customerId, int quantity, decimal price)
        {
            var item = await _itemsRepo.GetItemById(itemId);

            if (item == null) throw new InvalidOperationException("Item not found");

            if (quantity <= 0) throw new InvalidOperationException("Quantity must be positive");

            if (price <= 0) throw new InvalidOperationException("Price must be positive");

            item.DecreaseItemQuantityByGivenValue(quantity);
            await _itemsRepo.UpdateItem(item);
            var transaction = Transaction.CreateTransaction(item, new Customer(), -quantity, price);
            await _transactionsRepo.AddAsync(transaction);
        }
    }
}