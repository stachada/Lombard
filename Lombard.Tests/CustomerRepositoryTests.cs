using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Lombard.DAL;
using Lombard.DAL.Repositories;
using Lombard.BL.Models;
using Lombard.Tests.TestHelpers;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Lombard.Tests
{
    [TestFixture]
    class CustomerRepositoryTests
    {
        private IEnumerable<Customer> _customers;

        [OneTimeSetUp]
        public void Setup()
        {
            _customers = CustomerMockRepoHelper.GetCustomers();
        }

        [Test]
        public async Task AddCustomerToRepository_Test()
        {
            //Arrange
            var dbMock = new Mock<DbSet<Customer>>();

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(dbMock.Object);

            var customerRepo = new CustomersRepository(contextMock.Object);

            var customer = new Customer
            {
                CustomerId = 1,
                Name = "Bartek",
                BirthDate = new DateTime(1997, 5, 6)
            };

            //Act
            await customerRepo.AddCustomerAsync(customer);

            //Assert
            dbMock.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task GetAllCustomers_Test()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);
            var dbContextMock = new Mock<DatabaseContext>();
            dbContextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepo = new CustomersRepository(dbContextMock.Object);

            //Act
            var allCustomers = await customerRepo.GetAllAsync();

            //Assert
            Assert.AreEqual(3, allCustomers.Count());
        }

        [Test]
        public async Task GetCustomerById_Test()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);
            var dbContextMock = new Mock<DatabaseContext>();
            dbContextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepo = new CustomersRepository(dbContextMock.Object);

            //Act
            var customerWithGivenId = await customerRepo.GetCustomerByIdAsync(1);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(customerWithGivenId);
                Assert.AreEqual(1, customerWithGivenId.CustomerId);
                Assert.AreEqual("Bartek", customerWithGivenId.Name);
                Assert.AreEqual(new DateTime(1997, 5, 6), customerWithGivenId.BirthDate);
            });
        }

        [Test]
        public async Task UpdateCustomer_Test()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);
            var dbContextMock = new Mock<DatabaseContext>();
            dbContextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepo = new CustomersRepository(dbContextMock.Object);

            var customerToUpdate = new Customer()
            {
                CustomerId = 1,
                Name = "Michał",
                BirthDate = new DateTime(1997, 5, 6)
            };

            //Act
            await customerRepo.UpdateCustomerAsync(customerToUpdate);

            //Assert
            customerDbSetMock.Verify(m => m.Update(It.IsAny<Customer>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task DeleteCustomer_Test()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);
            var dbContextMock = new Mock<DatabaseContext>();
            dbContextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepo = new CustomersRepository(dbContextMock.Object);

            //Act
            await customerRepo.DeleteCustomerAsync(1);

            //Assert
            customerDbSetMock.Verify(m => m.Remove(It.IsAny<Customer>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
