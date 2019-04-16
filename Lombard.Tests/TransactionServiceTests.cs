using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using Moq;
using NUnit.Framework;
using System;
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
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.BuyAsync(1, 1, 10, 10.00M);

            mockItemsRepository.Verify(m => m.GetItemByIdAsync(1), Times.Once);
        }

        [Test]
        public void BuyAsync_NonExistingItem_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync((Item)null);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, 10, 10.00M));
        }

        [Test]
        public void BuyAsync_NonPositiveQuantity_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, -1, 10.00M));
        }

        [Test]
        public void BuyAsync_NonPositivePrice_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.BuyAsync(1, 1, 1, -10.00M));
        }

        [Test]
        public async Task BuyAsync_ShouldIncreaseItemsQuantity()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.BuyAsync(1, 1, 1, 10.00M);

            mockItem.Verify(m => m.IncreaseItemQuantityByGivenValue(1), Times.Once);
        }

        [Test]
        public async Task BuyAsync_UpdateItemInDb()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.BuyAsync(1, 1, 1, 10.00M);

            mockItemsRepository.Verify(m => m.UpdateItemAsync(It.IsAny<Item>()), Times.Exactly(2));
        }

        [Test]
        public async Task BuyAsync_ShouldAddTransactionToDb()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.BuyAsync(1, 1, 1, 10.00M);

            mockTransactionsRepository.Verify(m => m.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }

        [Test]
        public async Task SellAsync_GivenItemId_ShouldGetItemFromItemsRepository()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(new Item()
            {
                ItemId = 1,
                Price = 10.00M,
                Name = "Opona",
                Quantity = 5
            });
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.SellAsync(1, 1, 1, 10.00M);

            mockItemsRepository.Verify(m => m.GetItemByIdAsync(1), Times.Once);
        }

        [Test]
        public void SellAsync_NonExistingItem_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync((Item)null);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.SellAsync(1, 1, 10, 10.00M));
        }

        [Test]
        public void SellAsync_NonPositiveQuantity_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.SellAsync(1, 1, -1, 10.00M));
        }

        [Test]
        public void SellAsync_NonPositivePrice_ShouldThrowException()
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(new Item());
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => service.SellAsync(1, 1, 1, -10.00M));
        }

        [Test]
        public async Task SellAsync_ShouldDecreaseItemsQuantity()
        {
            var mockItem = new Mock<Item>();
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.SellAsync(1, 1, 1, 10.00M);

            mockItem.Verify(m => m.DecreaseItemQuantityByGivenValue(1), Times.Once);
        }

        [Test]
        public async Task SellAsync_UpdateItemInDb()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.SellAsync(1, 1, 1, 10.00M);

            mockItemsRepository.Verify(m => m.UpdateItemAsync(It.IsAny<Item>()), Times.Once);
        }

        [Test]
        public async Task SellAsync_ShouldAddTransactionToDb()
        {
            var mockItem = new Mock<Item>();
            mockItem.Setup(m => m.IncreaseItemQuantityByGivenValue(It.IsAny<int>()));
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).ReturnsAsync(mockItem.Object);
            var mockCustomerRepository = new Mock<ICustomersRepository>();
            mockCustomerRepository.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            var service = new TransactionsService(mockTransactionsRepository.Object, mockItemsRepository.Object, mockCustomerRepository.Object);

            await service.SellAsync(1, 1, 1, 10.00M);

            mockTransactionsRepository.Verify(m => m.AddAsync(It.IsAny<Transaction>()), Times.Once);
        }
    }
}
