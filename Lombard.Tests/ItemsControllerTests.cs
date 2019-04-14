using AutoMapper;
using Lombard.BL.Models;
using Lombard.BL.Services;
using LombardAPI.Controllers;
using LombardAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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
    }
}
