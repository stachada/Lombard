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
    class ItemServiceTests
    {
        [Test]
        public async Task CreateNewItem_GivenValidData_NewItemIsAdded()  
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            var itemToAdd = new Item
            {
                ItemId = 1,
                Price = 123.34M,
                Name = "Roll",
                Quantity = 15
            };

            await itemService.CreateNewItem(itemToAdd);

            mockItemsRepository.Verify(m => m.AddItem(It.IsAny<Item>()), Times.Once);
        }

        [TestCase(0)]
        [TestCase(-14)]
        public void CreateNewItem_InvalidPrice_InvalidOperationExceptionIsThrown(decimal price)
        {
            var mockItemsRepository = new Mock<IItemsRepository>();
            var itemService = new ItemService(mockItemsRepository.Object);

            var itemToAdd = new Item
            {
                ItemId = 1,
                Price = price,
                Name = "Roll",
                Quantity = 15
            };

            Assert.ThrowsAsync<InvalidOperationException>(() => itemService.CreateNewItem(itemToAdd));

            mockItemsRepository.Verify(m => m.AddItem(It.IsAny<Item>()), Times.Never);
        }
    }
}
