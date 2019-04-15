using System;
using Lombard.BL.Models;
using NUnit.Framework;

namespace Lombard.Tests
{
    class CustomerTests
    {
        [Test]
        public void Can_Change_Name_Test()
        {
            //Prepare
            Customer customer = new Customer()
            {
                Name = "Test"
            };
            string newName = "NewTest";

            //Act
            customer.ChangeName(newName);

            //Assert
            Assert.AreEqual(newName, customer.Name);
        }

        [Test]
        [TestCase(1924,12,30)]
        [TestCase(2001, 4, 10)]
        [TestCase(1997, 5, 6)]
        public void Is_Adult_Test(int year, int month, int day)
        {
            //Prepare
            Customer customer = new Customer()
            {
                BirthDate = new DateTime(year, month, day)
            };

            //Act

            //Assert
            Assert.IsTrue(customer.IsAdult());
        }

        [Test]
        [TestCase(2002, 4, 2)]
        [TestCase(2010, 1, 10)]
        [TestCase(2015, 11, 15)]
        public void Is_Not_Adult_Test(int year, int month, int day)
        {
            //Prepare
            Customer customer = new Customer()
            {
                BirthDate = new DateTime(year, month, day)
            };

            /*
            var customerMock = new Mock<Customer>();
            customerMock.Setup(c => c.BirthDate)
                .Returns(new DateTime(currentYear, currentMonth, currentDay));           
            Customer customer = customerMock.Object;
            */

            //Act

            //Assert
            Assert.IsFalse(customer.IsAdult());
        }
    }
}
