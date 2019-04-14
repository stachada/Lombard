using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using Lombard.DAL;
using Lombard.DAL.Repositories;
using Lombard.Tests.TestHelpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lombard.Tests
{
    [TestFixture]
    class CustomerServiceTests
    {

        private IEnumerable<Customer> _customers;

        [OneTimeSetUp]
        public void Setup()
        {
            _customers = CustomerMockRepoHelper.GetCustomers();
        }

        [Test]
        public async Task CreateNewCustomer_ValidData_Test1()
        {
            //Arrange
            var mockCustomersRepository = new Mock<ICustomersRepository>();
            var customerService = new CustomerService(mockCustomersRepository.Object);

            var customerToAdd = new Customer
            {
                CustomerId = 1,
                Name = "Bartek",
                BirthDate = new DateTime(1997, 5, 6)
            };

            //Act
            await customerService.CreateNewCustomerAsync(customerToAdd);

            //Assert
            mockCustomersRepository.Verify(m => m.AddCustomerAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Test]
        public void CreateNewCustomer_InvalidName_Test1()
        {
            //Arrange
            var mockCustomersRepository = new Mock<ICustomersRepository>();
            var customerService = new CustomerService(mockCustomersRepository.Object);

            var customerToAdd = new Customer
            {
                CustomerId = 1,
                Name = "",
                BirthDate = new DateTime(1997, 5, 6)
            };

            //Act and Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => customerService.CreateNewCustomerAsync(customerToAdd));
            mockCustomersRepository.Verify(m => m.AddCustomerAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Test]
        public void CreateNewCustomer_InvalidDate_Test1()
        {
            //Arrange
            var mockCustomersRepository = new Mock<ICustomersRepository>();
            var customerService = new CustomerService(mockCustomersRepository.Object);

            var customerToAdd = new Customer
            {
                CustomerId = 1,
                Name = "Bartek",
                BirthDate = new DateTime(2997, 5, 6)
            };

            //Act and Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => customerService.CreateNewCustomerAsync(customerToAdd));
            mockCustomersRepository.Verify(m => m.AddCustomerAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Test]
        public async Task DeleteCustomer_Existing_Test1()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepository = new CustomersRepository(contextMock.Object);

            //Act
            await customerRepository.DeleteCustomerAsync(1);

            //Assert
            customerDbSetMock.Verify(m => m.Remove(It.IsAny<Customer>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Test]
        public void DeleteCustomer_NotExisting_Test1()
        {
            //Arrage
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            
            var customerRepository = new CustomersRepository(contextMock.Object);

            //Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => customerRepository.DeleteCustomerAsync(20));
        }

        [Test]
        public async Task GetAllCustomers_Existing_Test1()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepository = new CustomersRepository(contextMock.Object);

            //Act
            var result = await customerRepository.GetAllAsync();

            //Assert
            Assert.AreEqual(3, result.Count());                  
        }

        [Test]
        public async Task GetCustomerById_Existing_Test1()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepository = new CustomersRepository(contextMock.Object);

            //Act
            var result = await customerRepository.GetCustomerByIdAsync(1);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Bartek", result.Name);

                Assert.AreEqual(new DateTime(1997,5,6), result.BirthDate);
            });
        }

        [Test]
        public async Task GetCustomerById_NotExisting_Test1()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var customerRepository = new CustomersRepository(contextMock.Object);

            //Act
            var result = await customerRepository.GetCustomerByIdAsync(20);

            //Assert
            Assert.Null(result);
        }

        [Test]
        public void UpdateItem_InvalidData_Test1()
        {
            //Arrange
            var mockCustomersRepository = new Mock<ICustomersRepository>();
            var customerService = new CustomerService(mockCustomersRepository.Object);

            var customerToUpdate = new Customer
            {
                CustomerId = 1,
                Name = "",
                BirthDate = new DateTime(1997, 5, 6)
            };

            //Action & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => customerService.UpdateCustomerAsync(customerToUpdate));

            //Assert
            mockCustomersRepository.Verify(m => m.UpdateCustomerAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Test]
        public async Task UpdateCustomera_Test1()
        {
            //Arrange
            var customerDbSetMock = CustomerMockRepoHelper.CreateDbSetMockForCustomers(_customers);

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(m => m.Customers).Returns(customerDbSetMock.Object);

            var repository = new CustomersRepository(contextMock.Object);

            var customerToUpdate = new Customer
            {
                CustomerId = 1,
                Name = "Michał",
                BirthDate = new DateTime(1997, 5, 6)
            };

            await repository.UpdateCustomerAsync(customerToUpdate);

            customerDbSetMock.Verify(m => m.Update(It.IsAny<Customer>()), Times.Once);
            contextMock.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
            
        }

    }
}
