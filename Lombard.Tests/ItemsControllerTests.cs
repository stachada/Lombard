using AutoMapper;
using Lombard.BL.Models;
using Lombard.BL.Services;
using LombardAPI.Controllers;
using LombardAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    [TestFixture]
    class ItemsControllerTests
    {
        [Test]
        public async Task UpdateItem_ItemExistsInDataBase_ItemIsUpdated()
        {
            // Arrange
            var itemDto = new ItemDto { ItemId = 1, Name = "Roll", Price = 123.5M, Quantity = 56 };
            var itemToUpdate = new Item { ItemId = 1, Name = "Stuff", Price = 15.2M, Quantity = 145 };
            var mockService = new Mock<IItemService>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Item>(itemDto))
           .Returns(itemToUpdate);

            mockService.Setup(m => m.UpdateItemAsync(itemToUpdate)).Returns(Task.FromResult(itemToUpdate));
            var controller = new ItemsController(mockService.Object, mockMapper.Object);

            // Act
            var result = await controller.UpdateItem(1,itemDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task UpdateItem_ItemDoesNotExist_ExcpetionIsTrhown()
        {
            // Arrange
            var itemDto = new ItemDto { ItemId = 1, Name = "Roll", Price = 123.5M, Quantity = 56 };
            var itemToUpdate = new Item { ItemId = 1, Name = "Stuff", Price = 15.2M, Quantity = 145 };
            var mockService = new Mock<IItemService>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Item>(itemDto))
           .Returns(itemToUpdate);

            mockService.Setup(m => m.UpdateItemAsync(itemToUpdate)).ThrowsAsync(new InvalidOperationException());
            var controller = new ItemsController(mockService.Object, mockMapper.Object);

            // Act
            var result = await controller.UpdateItem(1, itemDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateItem_GivenValidData_ShouldReturnCorrectActionResult()
        {
            var itemDto = new ItemDto { ItemId = 1, Name = "roll", Price = 123.5M, ProductCategory = "Different", Quantity = 152.6f };
            var mockItemService = new Mock<IItemService>();
            var itemToAdd = new Item { ItemId = 1, Name = "Stuff", Price = 15.2M, Quantity = 145 };

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Item>(itemDto))
           .Returns(itemToAdd);

            var controller = new ItemsController(mockItemService.Object, mockMapper.Object);

            var result = await controller.CreateItem(itemDto);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task CreateItem_InValidCategory_BadRequestReturned()
        {
            var itemDto = new ItemDto { ItemId = 1, Name = "roll", Price = 123.5M, ProductCategory = "Random", Quantity = 152.6f };
            var mockItemService = new Mock<IItemService>();

            var mockMapper = new Mock<IMapper>();
 

            var controller = new ItemsController(mockItemService.Object, mockMapper.Object);

            var result = await controller.CreateItem(itemDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task DeleteItem_ItemExistsInDatabase_ShouldReturnCorrectActionResult()
        {
            var mockItemService = new Mock<IItemService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new ItemsController(mockItemService.Object, mockMapper.Object);

            var result = await controller.DeleteItem(1);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteItem_ItemDoesNotExistInDatabase_ShouldReturnBadRequest()
        {
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(m => m.DeleteItemAsync(It.IsAny<int>())).ThrowsAsync(new InvalidOperationException());
            var mockMapper = new Mock<IMapper>();

            var controller = new ItemsController(mockItemService.Object, mockMapper.Object);

            var result = await controller.DeleteItem(1);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task GetItemById_ItemWithGivenIdIsReturned_ShouldReturnCorrectActionResult()
        {
            var mockItemService = new Mock<IItemService>();

            var mockMapper = new Mock<IMapper>();

            var controller = new ItemsController(mockItemService.Object, mockMapper.Object);

            var result = await controller.GetItemById(1);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
        }

        [Test]
        public async Task GetItemById_ItemWithGivenIdDoesNotExist_NotFoundStatusCode()
        {
            var mockItemService = new Mock<IItemService>();
            mockItemService.Setup(m => m.GetItemByIdAsync(1)).ThrowsAsync(new InvalidOperationException());
            var mockMapper = new Mock<IMapper>();

            var controller = new ItemsController(mockItemService.Object, mockMapper.Object);

            var result = await controller.GetItemById(1);

            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        }
    }
}
