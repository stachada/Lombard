using Lombard.BL.Models;
using Lombard.DAL;
using Lombard.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    [TestFixture]
    public class TransactionRepositoryTests
    {
        private IEnumerable<Transaction> _transactions;

        [SetUp]
        public void Setup()
        {
            _transactions = GenerateTransactions();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllTransactions()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetAllAsync();

            Assert.That(3, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetById_GivenId_ReturnsTheCorrectTransaction()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetByIdAsync(2);

            Assert.Multiple(() =>
            {
                Assert.That(null, Is.Not.EqualTo(result));
                Assert.That(2, Is.EqualTo(result.Quantity));
                Assert.That("Customer1", Is.EqualTo(result.Customer.Name));
                Assert.That("Udko z psa", Is.EqualTo(result.Item.Name));
            });
        }

        [Test]
        public async Task GetById_NonexistingId_ReturnsNull()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetByIdAsync(5);

            Assert.That(null, Is.EqualTo(result));
        }

        [Test]
        public async Task AddAsync_GivenTransaction_ShouldCallAddOnDbSetAndSaveChangesAsyncOnDbContext()
        {
            Mock<DbSet<Transaction>> dbSetMock = new Mock<DbSet<Transaction>>();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1);

            await repository.AddAsync(transaction);

            dbSetMock.Verify(m => m.Add(It.IsAny<Transaction>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_GivenTransaction_ShouldCallUpdateOnDbSetAndSaveChangesAsyncOnDbContext()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1, 1);

            await repository.UpdateAsync(transaction);

            dbSetMock.Verify(m => m.Update(It.IsAny<Transaction>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void UpdateAsync_NonExistingTransaction_ShouldThrowException()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1, 5);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.UpdateAsync(transaction));
        }

        [Test]
        public async Task DeleteAsync_GivenTransactionId_ShouldCallRemoveOnDbSetAndSaveChangesAsyncOnDbContext()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            await repository.DeleteAsync(1);

            dbSetMock.Verify(m => m.Remove(It.IsAny<Transaction>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void DeleteAsync_NonExistingTransactionId_ShouldThrowException()
        {
            Mock<DbSet<Transaction>> dbSetMock = CreateDbSetMockForTransaction();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.DeleteAsync(5));
        }

        private Mock<DbSet<Transaction>> CreateDbSetMockForTransaction()
        {
            var dbSetMock = new Mock<DbSet<Transaction>>();
            dbSetMock.As<IAsyncEnumerable<Transaction>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Transaction>(_transactions.AsQueryable().GetEnumerator()));

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Transaction>(_transactions.AsQueryable().Provider));

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.Expression)
                .Returns(_transactions.AsQueryable().Expression);

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.ElementType)
                .Returns(_transactions.AsQueryable().ElementType);

            dbSetMock.As<IQueryable<Transaction>>()
                .Setup(m => m.GetEnumerator())
                .Returns(_transactions.AsQueryable().GetEnumerator());
            return dbSetMock;
        }

        private IEnumerable<Transaction> GenerateTransactions()
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

            var transactions = new List<Transaction>()
            {
                Transaction.CreateTransaction(item1, customer1, 1, 1),
                Transaction.CreateTransaction(item2, customer1, 2, 2),
                Transaction.CreateTransaction(item3, customer2, 4, 3)
            };

            return transactions;
        }
    }
}
