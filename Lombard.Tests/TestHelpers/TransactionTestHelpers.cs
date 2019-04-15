using Lombard.BL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lombard.Tests
{
    internal static class TransactionTestHelpers
    {
        internal static Mock<DbSet<Transaction>> CreateDbSetMockForTransaction(IEnumerable<Transaction> transactions)
        {
            var dbSetMock = new Mock<DbSet<Transaction>>();
            dbSetMock.As<IAsyncEnumerable<Transaction>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Transaction>(transactions.AsQueryable().GetEnumerator()));

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Transaction>(transactions.AsQueryable().Provider));

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.Expression)
                .Returns(transactions.AsQueryable().Expression);

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.ElementType)
                .Returns(transactions.AsQueryable().ElementType);

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.GetEnumerator())
                .Returns(transactions.AsQueryable().GetEnumerator());
            return dbSetMock;
        }

        internal static IEnumerable<Transaction> GenerateTransactions()
        {
            var customer1 = new Customer()
            {
                CustomerId = 1,
                Name = "Customer1",
                BirthDate = new DateTime(1990, 12, 12)
            };

            var customer2 = new Customer()
            {
                CustomerId = 2,
                Name = "Customer2",
                BirthDate = new DateTime(1991, 05, 14)
            };

            var item1 = new Item()
            {
                ItemId = 1,
                Price = 10.00M,
                Name = "Opona",
                Quantity = 5
            };

            var item2 = new Item()
            {
                ItemId = 2,
                Price = 15.55M,
                Name = "Udko z psa",
                Quantity = 10
            };

            var item3 = new Item()
            {
                ItemId = 3,
                Price = 5.35M,
                Name = "Paczka ryżu",
                Quantity = 7
            };

            var transaction1 = Transaction.CreateTransaction(item1, customer1, 1, 10.00M, 1);
            transaction1.SetTransactionDate(new DateTime(2019, 3, 1));

            var transaction2 = Transaction.CreateTransaction(item2, customer1, 2, 15.00M, 2);
            transaction2.SetTransactionDate(new DateTime(2019, 4, 1));

            var transaction3 = Transaction.CreateTransaction(item3, customer2, 4, 5.00M, 3);
            transaction3.SetTransactionDate(new DateTime(2019, 4, 2));

            var transaction4 = Transaction.CreateTransaction(item1, customer2, -1, 11.00M, 4);
            transaction4.SetTransactionDate(new DateTime(2019, 3, 2));

            var transaction5 = Transaction.CreateTransaction(item2, customer2, -1, 20.05M, 5);
            transaction5.SetTransactionDate(new DateTime(2019, 4, 3));

            var transaction6 = Transaction.CreateTransaction(item3, customer1, -3, 6.45M, 6);
            transaction6.SetTransactionDate(new DateTime(2019, 4, 4));

            var transactions = new List<Transaction>()
            {
                transaction1,
                transaction2,
                transaction3,
                transaction4,
                transaction5,
                transaction6
            };

            return transactions;
        }
    }
}
