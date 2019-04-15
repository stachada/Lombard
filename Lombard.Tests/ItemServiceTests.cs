using Lombard.BL.Helpers;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using Lombard.Tests.TestHelpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    [TestFixture]
    class ItemServiceTests
    {
        [Test]
        public async Task CreateNewItem_GivenValidData_NewItemIsAdded()
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            var itemToAdd = new Item
            {
                ItemId = 1,
                Price = 123.34M,
                Name = "Roll",
                Quantity = 15
            };

            //Action
            await itemService.CreateNewItemAsync(itemToAdd);

            //Assert
            mockItemsRepository.Verify(m => m.AddItemAsync(It.IsAny<Item>()), Times.Once);
        }

        [TestCase(0)]
        [TestCase(-14)]
        public void CreateNewItem_InvalidPrice_InvalidOperationExceptionIsThrown(decimal price)
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            var itemToAdd = new Item
            {
                ItemId = 1,
                Price = price,
                Name = "Roll",
                Quantity = 15
            };
            // Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.CreateNewItemAsync(itemToAdd));

            mockItemsRepository.Verify(m => m.AddItemAsync(It.IsAny<Item>()), Times.Never);
        }

        [TestCase("")]
        [TestCase(null)]
        public void CreateNewItem_InvalidName_InvalidOperationExceptionIsThrown(string name)
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            var itemToAdd = new Item
            {
                ItemId = 1,
                Price = 123.50M,
                Name = name,
                Quantity = 15
            };

            //Action and Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.CreateNewItemAsync(itemToAdd));

            mockItemsRepository.Verify(m => m.AddItemAsync(It.IsAny<Item>()), Times.Never);
        }

        [TestCase(0)]
        [TestCase(-14)]
        public void CreateNewItem_InvalidQuantity_InvalidOperationExceptionIsThrown(int quantity)
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            var itemToAdd = new Item
            {
                ItemId = 1,
                Price = 123.50M,
                Name = "Roll",
                Quantity = quantity
            };

            //Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.CreateNewItemAsync(itemToAdd));

            mockItemsRepository.Verify(m => m.AddItemAsync(It.IsAny<Item>()), Times.Never);
        }

        [Test]
        public async Task DeleteItem_ValidItemId_ItemIsDeleted()
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            //Action
            await itemService.DeleteItemAsync(12);

            //Assert
            mockItemsRepository.Verify(m => m.DeleteItemAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task GetAllItems_ItemsExistsInDatabase_AllItemsareReturned()
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetAllAsync(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(ItemConfigurator.GenerateItems());

            var itemService = new ItemService(mockItemsRepository.Object);

            //Action
            var allItems = await itemService.GetAllItemsAsync(1,2);

            //Assert
            mockItemsRepository.Verify(m => m.GetAllAsync(1,2), Times.Once);

            Assert.AreEqual(3, allItems.Count());
        }

        [Test]
        public async Task GetAllItems_DatabaseIsEmpty_NoItemsAreReturned()
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new PagedList<Item>(new List<Item>(),0,0,0));

            var itemService = new ItemService(mockItemsRepository.Object);

            //Action
            var allItems = await itemService.GetAllItemsAsync(1,2);

            //Assert
            mockItemsRepository.Verify(m => m.GetAllAsync(1,2), Times.Once);

            Assert.IsEmpty(allItems);
        }

        [Test]
        public async Task GetItemById_ItemExistsInDatabase_ItemWithGivenIdExists()
        {
            //Arrange
            var item = new Item { ItemId = 1 };

            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(item));

            var itemService = new ItemService(mockItemsRepository.Object);

            //Action
            var itemWithGivenId = await itemService.GetItemByIdAsync(1);

            //Assert
            mockItemsRepository.Verify(m => m.GetItemByIdAsync(It.IsAny<int>()), Times.Once);

            Assert.AreEqual(itemWithGivenId.ItemId, 1);
        }

        [Test]
        public async Task GetItemById_ItemDoesNotExistInDatabase_NullValueIsReturned()
        {
            //Arrange
            Item item = null;

            var mockItemsRepository = new Mock<IItemsRepository>();
             mockItemsRepository.Setup(m => m.GetItemByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(item));

            var itemService = new ItemService(mockItemsRepository.Object);

            //Action
            var itemWithGivenId = await itemService.GetItemByIdAsync(1);

            //Assert
            mockItemsRepository.Verify(m => m.GetItemByIdAsync(It.IsAny<int>()), Times.Once);

            Assert.IsNull(itemWithGivenId);
        }

        [TestCase(0)]
        [TestCase(-12)]
        public void GetItemById_GivenIdIsInvalid_ExceptionIsThrown(int itemId)
        {
            //Arrange

            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            //Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.GetItemByIdAsync(itemId));

            mockItemsRepository.Verify(m => m.GetItemByIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public async Task UpdateItem_ItemExistsInDatabase_ItemGetsUpdated()
        {
            //Arrange
            var itemToUpdate = new Item { ItemId = 1, Name = "Tire", Price = 123.5M, Quantity = 5434 };
            var mockItemsRepository = new Mock<IItemsRepository>();
            mockItemsRepository.Setup(m => m.UpdateItemAsync(It.IsAny<Item>())).Callback((Item item) => item.Name = "Rice").Returns(Task.FromResult(itemToUpdate));

            var itemService = new ItemService(mockItemsRepository.Object);

            //Action
            await itemService.UpdateItemAsync(itemToUpdate);

            //Assert
            mockItemsRepository.Verify(m => m.UpdateItemAsync(It.IsAny<Item>()), Times.Once);
            Assert.AreEqual("Rice", itemToUpdate.Name);

        }

        [TestCase(0)]
        [TestCase(-123.54)]
        public void UpdateItem_InvalidPrice_ExceptionIsThrown(decimal price)
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            //Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() =>itemService.UpdateItemAsync(new Item { ItemId = 1, Quantity = 213, Price = price, Name = "tire" }));

            //Assert
            mockItemsRepository.Verify(m => m.UpdateItemAsync(It.IsAny<Item>()), Times.Never);

        }

        [TestCase("")]
        [TestCase(null)]
        public void UpdateItem_InvalidName_ExceptionIsThrown(string name)
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            //Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.UpdateItemAsync(new Item { ItemId = 1, Quantity = 213, Price = 123M, Name = name }));

            //Assert
            mockItemsRepository.Verify(m => m.UpdateItemAsync(It.IsAny<Item>()), Times.Never);

        }

        [TestCase(0)]
        [TestCase(-5)]
        public void UpdateItem_InvalidQuantity_ExceptionIsThrown(int quantity)
        {
            //Arrange
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            //Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.UpdateItemAsync(new Item { ItemId = 1, Quantity = quantity, Price = 123M, Name = "tire" }));

            //Assert
            mockItemsRepository.Verify(m => m.UpdateItemAsync(It.IsAny<Item>()), Times.Never);

        }
    }
}
