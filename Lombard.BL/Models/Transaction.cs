using System;
using System.Collections.Generic;
using System.Text;

namespace Lombard.BL.Models
{
    public class Transaction
    {
        public static Transaction CreateTransaction(Item item, Customer customer, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("quantity");

            if (customer == null)
                throw new InvalidOperationException("customer");

            if (item == null)
                throw new InvalidOperationException("item");

            if (quantity > item.Quantity)
                throw new InvalidOperationException("quantity cannot be less than item's quantity");

            Transaction transaction = new Transaction();
            transaction.Item = item;
            transaction.Customer = customer;
            transaction.Quantity = quantity;

            return transaction;
        }

        private Transaction()
        {

        }

        public DateTime TransactionDate { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public Item Item { get; private set; }
        public Customer Customer { get; private set; }

        public decimal GetTransactionAmount()
        {
            return Item.Price * Quantity;
        }

    }
}
