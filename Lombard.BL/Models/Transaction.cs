using System;
using System.Collections.Generic;
using System.Text;

namespace Lombard.BL.Models
{
    public class Transaction
    {
        public static Transaction CreateTransaction(Item item, Customer customer, int quantity)
        {
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
            throw new NotImplementedException();
        }

    }
}
