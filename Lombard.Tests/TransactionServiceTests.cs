using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    [TestFixture]
    public class TransactionServiceTests
    {
        [Test]
        public async Task BuyAsync_GivenItemId_ShouldGetItemFromItemsRepository()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).Returns(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            await service.BuyAsync(1, 1, 10, 10.00M);

            mockItemsRepository.Verify(m => m.GetItemById(1), Times.Once);
        }

        [Test]
        public void BuyAsync_NonExistingItem_ShouldShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).Returns((Item)null);
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, 10, 10.00M));
        }

        [Test]
        public void BuyAsync_NonPositiveQuantity_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).Returns(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, -1, 10.00M));
        }

        [Test]
        public void BuyAsync_NonPositivePrice_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).Returns(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, 1, -10.00M));
        }
    }
}
