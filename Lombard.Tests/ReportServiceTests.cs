using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    [TestFixture]
    public class ReportServiceTests
    {
        [Test]
        public async Task GetTurnover_CallsApproriateMethodInTransactionsRepository()
        {
            var mockTransactionsRepo = new Mock<ITransactionsRepository>();

            var mockItemsRepo = new Mock<IItemsRepository>();

            var service = new ReportService(mockTransactionsRepo.Object, mockItemsRepo.Object);
            var start = new DateTime(2019, 3, 1);
            var end = new DateTime(2019, 3, 31);

            var result = await service.GetTurnover(start, end);

            mockTransactionsRepo.Verify(m => m.GetTurnover(start, end), Times.Once);
        }

        [Test]
        public async Task GetProfit_CallsApproriateMethodInTransactionsRepository()
        {
            var mockTransactionsRepo = new Mock<ITransactionsRepository>();

            var mockItemsRepo = new Mock<IItemsRepository>();

            var service = new ReportService(mockTransactionsRepo.Object, mockItemsRepo.Object);
            var start = new DateTime(2019, 3, 1);
            var end = new DateTime(2019, 3, 31);

            var result = await service.GetProfit(start, end);

            mockTransactionsRepo.Verify(m => m.GetProfit(start, end), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_CallsApproriateMethodOnItemsRepository()
        {
            var mockTransactionsRepo = new Mock<ITransactionsRepository>();

            var mockItemsRepo = new Mock<IItemsRepository>();

            var service = new ReportService(mockTransactionsRepo.Object, mockItemsRepo.Object);

            var result = await service.GetAllAsync(1,2);

            mockItemsRepo.Verify(m => m.GetAllAsync(1,2), Times.Once);
        }

        [Test]
        public async Task GetQuantityInCategories_CallsApproriateMethodOnItemsRepository()
        {
            var mockTransactionsRepo = new Mock<ITransactionsRepository>();

            var mockItemsRepo = new Mock<IItemsRepository>();

            var service = new ReportService(mockTransactionsRepo.Object, mockItemsRepo.Object);

            var result = await service.GetQuantityInCategoriesAsync();

            mockItemsRepo.Verify(m => m.GetQuantityInCategoriesAsync(), Times.Once);
        }


        [Test]
        public async Task GetItemsWithQuantityLowerThanAsync_CallsApproriateMethodOnItemsRepository()
        {
            var mockTransactionsRepo = new Mock<ITransactionsRepository>();

            var mockItemsRepo = new Mock<IItemsRepository>();

            var service = new ReportService(mockTransactionsRepo.Object, mockItemsRepo.Object);
            var quantity = 10.44f;

            var result = await service.GetItemsWithQuantityLowerThanAsync(quantity);

            mockItemsRepo.Verify(m => m.GetItemsWithQuantityLowerThanAsync(quantity), Times.Once);
        }
    }
}
