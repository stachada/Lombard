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
            _transactions = TransactionTestHelpers.GenerateTransactions();
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllTransactions()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetAllAsync();

            Assert.That(3, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetById_GivenId_ReturnsTheCorrectTransaction()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

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
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

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

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1, 10.00M);

            await repository.AddAsync(transaction);

            dbSetMock.Verify(m => m.Add(It.IsAny<Transaction>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_GivenTransaction_ShouldCallUpdateOnDbSetAndSaveChangesAsyncOnDbContext()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1, 10.00M, 1);

            await repository.UpdateAsync(transaction);

            dbSetMock.Verify(m => m.Update(It.IsAny<Transaction>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void UpdateAsync_NonExistingTransaction_ShouldThrowException()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1, 10.00M, 5);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.UpdateAsync(transaction));
        }

        [Test]
        public async Task DeleteAsync_GivenTransactionId_ShouldCallRemoveOnDbSetAndSaveChangesAsyncOnDbContext()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

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
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.DeleteAsync(5));
        }

        
    }
}
