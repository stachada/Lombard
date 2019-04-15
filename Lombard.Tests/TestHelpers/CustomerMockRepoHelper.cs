using Lombard.BL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lombard.Tests.TestHelpers
{
    public class CustomerMockRepoHelper
    {
        public static Mock<DbSet<Customer>> CreateDbSetMockForCustomers(IEnumerable<Customer> customers)
        {
            var dbSetMock = new Mock<DbSet<Customer>>();
            dbSetMock.As<IAsyncEnumerable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Customer>(customers.AsQueryable().GetEnumerator()));

            dbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Customer>(customers.AsQueryable().Provider));

            dbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.Expression)
                .Returns(customers.AsQueryable().Expression);

            dbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.ElementType)
                .Returns(customers.AsQueryable().ElementType);

            dbSetMock.As<IQueryable<Customer>>()
                .Setup(m => m.GetEnumerator())
                .Returns(customers.AsQueryable().GetEnumerator());
            return dbSetMock;
        }

        public static IEnumerable<Customer> GetCustomers()
        {
            var Customers = new List<Customer>()
            {
                new Customer(){CustomerId = 1, Name = "Bartek", BirthDate = new DateTime(1997,5,6)},
                new Customer(){CustomerId = 2, Name = "Adam", BirthDate = new DateTime(1995,2,20)},
                new Customer(){CustomerId = 3, Name = "Leszek", BirthDate = new DateTime(1984,10,16)}
            };

            return Customers;
        }
    }
}
