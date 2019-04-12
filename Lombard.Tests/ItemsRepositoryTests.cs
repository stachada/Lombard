using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Lombard.DAL;
using Lombard.BL.Models;
using Lombard.Tests.TestHelpers;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Lombard.DAL.Repositories;
using System.Linq;
using System;

namespace Lombard.Tests
{
    [TestFixture]
    class ItemsRepositoryTests
    {
        private IEnumerable<Item> _items;

        [OneTimeSetUp]
        public void Setup()
        {
            _items = ItemConfigurator.GenerateItems();
        }

        [Test]
        public async Task AddItemToRepository_ItemIsAdded_AddAndSaveChangesAreCalled()
        {
            //Arrange
            var dbMock = new Mock<DbSet<Item>>();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(dbMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            var item = new Item
            {
                ItemId = 1,
                Price = 123.70M,
                Name = "Cup",
                Quantity = 15
            };

            //Action
            await itemRepo.AddItem(item);

            //Assert
            dbMock.Verify(m => m.Add(It.IsAny<Item>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void GetAllItems_AllItemsInDatabaseAreReturned_CountOfItemsMatchesGivenValue()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            //Action
            var allItems = itemRepo.GetAll();

            //Assert
            Assert.AreEqual(3, allItems.Count());
        }

        [Test]
        public void GetAllItems_DatabaseIsEmpty_NoItemsAreReturned()
        {
            //Arrange
            var dbMock = ItemConfigurator.CreateDbSetMockForItems(new List<Item>());
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(dbMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            //Action
            var allItems = itemRepo.GetAll();

            //Assert
            Assert.IsEmpty(allItems);
        }

        [Test]
        public async Task GetItemById_ItemExistsInDatabase_ItemWithGivenIdIsReturned()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            //Action
            var itemWithGivenId = await itemRepo.GetItemById(1);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(itemWithGivenId);
                Assert.AreEqual(1, itemWithGivenId.ItemId);
                Assert.AreEqual(25.10M, itemWithGivenId.Price);
                Assert.AreEqual("Tire", itemWithGivenId.Name);
                Assert.AreEqual(6, itemWithGivenId.Quantity);
            });
        }

        [Test]
        public async Task GetItemById_ItemWithGivenIdDoesNotExists_NullIsReturned()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            //Action
            var itemWithGivenId = await itemRepo.GetItemById(-14);

            //Assert
            Assert.IsNull(itemWithGivenId);
        }

        [Test]
        public async Task UpdateItem_ItemExistsInDatabase_UpdateOnDbAndSaveChangesShouldBeCalled()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            var ItemToUpdate = new Item
            {
                ItemId = 1,
                Price = 1234.456M,
                Name = "Umbrella",
                Quantity = 18
            };

            //Action
            await itemRepo.UpdateItem(ItemToUpdate);

            //Assert
            databaseSetMock.Verify(m => m.Update(It.IsAny<Item>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void UpdateItem_ItemDoesNotExistInDatabase_ItemDoesNotExistExceptionIsThrown()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            var ItemToUpdate = new Item
            {
                ItemId = -14,
                Price = 1234.456M,
                Name = "Cat",
                Quantity = 1
            };

            //Action && Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemRepo.UpdateItem(ItemToUpdate));

            //Assert
            databaseSetMock.Verify(m => m.Update(It.IsAny<Item>()), Times.Never);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

        [Test]
        public async Task DeleteItem_ItemWithGivenIdExistInDatabase_ItemIsDeleted()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            //Action
            await itemRepo.DeleteItem(1);

            //Assert
            databaseSetMock.Verify(m => m.Remove(It.IsAny<Item>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void DeleteItem_ItemWithGivenIdDoesNotExistInDatabase_ExceptionIsThrown()
        {
            //Arrange
            var databaseSetMock = ItemConfigurator.CreateDbSetMockForItems(_items);
            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Items).Returns(databaseSetMock.Object);

            var itemRepo = new ItemsRepository(contextMock.Object);

            //Action && Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => itemRepo.DeleteItem(-14));

            //Assert
            databaseSetMock.Verify(m => m.Remove(It.IsAny<Item>()), Times.Never);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Never);
        }
    }
}
