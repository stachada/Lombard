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
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            await service.BuyAsync(1, 1, 10, 10.00M);

            mockItemsRepository.Verify(m => m.GetItemById(1), Times.Once);
        }

        [Test]
        public void BuyAsync_NonExistingItem_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync((Item)null);
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, 10, 10.00M));
        }

        [Test]
        public void BuyAsync_NonPositiveQuantity_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, -1, 10.00M));
        }

        [Test]
        public void BuyAsync_NonPositivePrice_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, 1, -10.00M));
        }

        [Test]
        public async Task BuyAsync_ShouldIncreaseItemsQuantity()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            await service.BuyAsync(1, 1, 1, 10.00M);

            mockItem.Verify(m => m.IncreaseItemQuantityByGivenValue(1), Times.Once);
        }

        [Test]
        public async Task BuyAsync_UpdateItemInDb()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            await service.BuyAsync(1, 1, 1, 10.00M);

            mockItemsRepository.Verify(m => m.UpdateItem(It.IsAny<Item>()), Times.Once);
        }

        [Test]
        public async Task BuyAsync_ShouldAddTransactionToDb()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemById(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object);

            await service.BuyAsync(1, 1, 1, 10.00M);

            mockTransactionsRepository.Verify(m => m.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
