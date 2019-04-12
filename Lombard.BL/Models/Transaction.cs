using System;

namespace Lombard.BL.Models
{
    public class Transaction
    {
        public static Transaction CreateTransaction(Item item, Customer customer, int quantity, decimal price, int id = 0)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("quantity");

            if (customer == null)
                throw new InvalidOperationException("customer");

            if (item == null)
                throw new InvalidOperationException("item");

            Transaction transaction = new Transaction();
            transaction.TransactionId = id;
            transaction.Item = item;
            transaction.Customer = customer;
            transaction.Quantity = quantity;
            transaction.Price = price;

            return transaction;
        }

        private Transaction()
        {

        }

        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public Item Item { get; private set; }
        public Customer Customer { get; private set; }


        public decimal GetTransactionAmount()
        {
            return Item.Price * Quantity;
        }

        public void SetTransactionDate(DateTime date)
        {
            TransactionDate = date;
        }
    }
}
