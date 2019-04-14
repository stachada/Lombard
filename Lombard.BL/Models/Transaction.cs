﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.BL.Models
{
    public class Transaction
    {
        public static Transaction CreateTransaction(Item item, Customer customer, float quantity, decimal price, int id = 0)
        {
            if (price <= 0)
                throw new InvalidOperationException("price");

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

        [Required]
        public DateTime TransactionDate { get; private set; }

        [Required]
        public float Quantity { get; private set; }

        [Required]
        public decimal Price { get; private set; }

        [Required]
        public Item Item { get; private set; }

        [NotMapped]
        public bool IsPurchase => Quantity >= 0;

        public Customer Customer { get; private set; }

        /// <summary>
        /// Calculates the amount of transaction
        /// </summary>
        /// <returns>
        /// The negative value for purchase and positive value for sales
        /// </returns>
        public decimal GetTransactionAmount()
        {
            return -1 * Price * (decimal)Quantity;
        }

        public void SetTransactionDate(DateTime date)
        {
            TransactionDate = date;
        }
    }
}
