using AutoMapper;
using Lombard.BL.Helpers;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using LombardAPI.Controllers;
using LombardAPI.Dtos;
using LombardAPI.Queries;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Lombard.Tests
{
    [TestFixture]
    public class TransactionsControllerTests
    {
        [Test]
        public async Task GetTransactions_GivenTransactionsQuery_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            mockTransactionsRepository.Setup(m => m.GetTransactions(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedList<Transaction>(GenerateTransactions(), 1, 1, 1));
            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var query = new TransactionsQuery
            {
                PageNumber = 1,
                PageSize = 5
            };

            var result = await controller.GetTransactions(query);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetTransactions_GivenTransactionsQueryNoDataReturned_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            mockTransactionsRepository.Setup(m => m.GetTransactions(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PagedList<Transaction>(new List<Transaction>(), 1, 1, 1));
            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var query = new TransactionsQuery
            {
                PageNumber = 1,
                PageSize = 5
            };

            var result = await controller.GetTransactions(query);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Get_GivenId_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            mockTransactionsRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(Transaction.CreateTransaction(new Item(), new Customer(), 1, 1));
            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var result = await controller.Get(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task Get_GivenIdNoDataReturned_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();
            mockTransactionsRepository.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Transaction)null);
            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var result = await controller.Get(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Buy_GivenTransactionData_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();
            mockTransactionsService.Setup(m => m.BuyAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .ReturnsAsync(Transaction.CreateTransaction(new Item(), new Customer(), 1, 1.00M));

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var transactionDto = new TransactionDto
            {
                TransactionId = 1,
                ItemId = 1,
                CustomerId = 1,
                Quantity = 1,
                Price = 1.00M,
                TransactionDate = new DateTime(2019, 4, 14)
            };

            var result = await controller.Buy(transactionDto);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task Buy_ServiceThrowsInvalidOperationException_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();
            mockTransactionsService.Setup(m => m.BuyAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .ThrowsAsync(new InvalidOperationException());

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var transactionDto = new TransactionDto
            {
                TransactionId = 1,
                ItemId = 1,
                CustomerId = 1,
                Quantity = 1,
                Price = 1.00M,
                TransactionDate = new DateTime(2019, 4, 14)
            };

            var result = await controller.Buy(transactionDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Sell_GivenTransactionData_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();
            mockTransactionsService.Setup(m => m.SellAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .ReturnsAsync(Transaction.CreateTransaction(new Item(), new Customer(), 1, 1.00M));

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var transactionDto = new TransactionDto
            {
                TransactionId = 1,
                ItemId = 1,
                CustomerId = 1,
                Quantity = 1,
                Price = 1.00M,
                TransactionDate = new DateTime(2019, 4, 14)
            };

            var result = await controller.Sell(transactionDto);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task Sell_ServiceThrowsInvalidOperationException_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();
            mockTransactionsService.Setup(m => m.SellAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>()))
                .ThrowsAsync(new InvalidOperationException());

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var transactionDto = new TransactionDto
            {
                TransactionId = 1,
                ItemId = 1,
                CustomerId = 1,
                Quantity = 1,
                Price = 1.00M,
                TransactionDate = new DateTime(2019, 4, 14)
            };

            var result = await controller.Sell(transactionDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Put_GivenUpdatedTransactionData_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var transactionDto = new TransactionDto
            {
                TransactionId = 1,
                ItemId = 1,
                CustomerId = 1,
                Quantity = 1,
                Price = 1.00M,
                TransactionDate = new DateTime(2019, 4, 14)
            };

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var result = await controller.Put(1, transactionDto);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Put_ServiceThrowsInvalidOperationException_ShouldReturnCorrectionActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();
            mockTransactionsService.Setup(m => m.UpdateTransactionAsync(It.IsAny<Transaction>()))
                .ThrowsAsync(new InvalidOperationException());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var transactionDto = new TransactionDto
            {
                TransactionId = 1,
                ItemId = 1,
                CustomerId = 1,
                Quantity = 1,
                Price = 1.00M,
                TransactionDate = new DateTime(2019, 4, 14)
            };

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var result = await controller.Put(1, transactionDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task Delete_GivenId_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();

            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var result = await controller.Delete(1);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task Delete_ServiceThrowsInvalidOperationException_ShouldReturnCorrectActionResult()
        {
            var mockTransactionsService = new Mock<ITransactionsService>();
            mockTransactionsService.Setup(m => m.DeleteTransactionAsync(It.IsAny<int>()))
                .ThrowsAsync(new InvalidOperationException());
            var mockTransactionsRepository = new Mock<ITransactionsRepository>();

            var mockMapper = new Mock<IMapper>();

            var controller = new TransactionsController(
                mockTransactionsService.Object,
                mockTransactionsRepository.Object,
                mockMapper.Object);

            var result = await controller.Delete(1);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        private List<Transaction> GenerateTransactions()
        {
            var transactions = new List<Transaction>
            {
                Transaction.CreateTransaction(new Item(), new Customer(), 1, 10),
                Transaction.CreateTransaction(new Item(), new Customer(), 1, 10),
                Transaction.CreateTransaction(new Item(), new Customer(), 1, 10)
            };

            return transactions;
        }
    }
}
