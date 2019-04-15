using Lombard.BL.Models;
using Lombard.DAL;
using Lombard.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 2)]
        [TestCase(3, 2, 2)]
        [TestCase(2, 4, 2)]
        public async Task GetTransactions_GivenPageNumberAndPageSize_ShouldReturnCorrectData(
            int pageNumber, int pageSize, int expectedResult)
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetTransactions(pageNumber, pageSize);

            Assert.That(result.Count, Is.EqualTo(expectedResult));
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

            var result = await repository.GetByIdAsync(7);

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
        public void UpdateAsync_NonExistingTransaction_ShouldThrowException()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var transaction = Transaction.CreateTransaction(new Item { Quantity = 1 }, new Customer(), 1, 10.00M, 7);

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

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.DeleteAsync(7));
        }

        [Test]
        public async Task GetTurnover_InApril2019_ShouldReturnAProperValue()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetTurnover(new DateTime(2019, 4, 1), new DateTime(2019, 4, 30));

            Assert.That(result, Is.EqualTo(39.40M));
        }

        [Test]
        public async Task GetTurnover_InMarch2019_ShouldReturnAProperValue()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetTurnover(new DateTime(2019, 3, 1), new DateTime(2019, 3, 31));

            Assert.That(result, Is.EqualTo(11.00M));
        }

        [Test]
        public void GetTurnover_StartLaterThanEnd_ShouldThrowException()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.GetTurnover(new DateTime(2019, 5, 1), new DateTime(2019, 3, 31)));
        }

        [Test]
        public async Task GetProfit_InApril2019_ShouldReturnAProperValue()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetProfit(new DateTime(2019, 4, 1), new DateTime(2019, 4, 30));

            Assert.That(result, Is.EqualTo(-10.60M));
        }

        [Test]
        public async Task GetProfit_InMarch2019_ShouldReturnAProperValue()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            var result = await repository.GetProfit(new DateTime(2019, 3, 1), new DateTime(2019, 3, 31));

            Assert.That(result, Is.EqualTo(1.00M));
        }

        [Test]
        public void GetProfit_StartLaterThanEnd_ShouldThrowException()
        {
            Mock<DbSet<Transaction>> dbSetMock = TransactionTestHelpers.CreateDbSetMockForTransaction(_transactions);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Transactions).Returns(dbSetMock.Object);

            var repository = new TransactionsRepository(contextMock.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => repository.GetProfit(new DateTime(2019, 5, 1), new DateTime(2019, 3, 31)));
        }
    }
}
