using System;
using System.Collections.Generic;
using System.Text;
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
        [TestCase(2012, 12, 30)]
        [TestCase(2008, 4, 10)]
        [TestCase(2018, 5, 6)]
        public void Is_Not_Adult_Test(int year, int month, int day)
        {
            //Prepare
            Customer customer = new Customer()
            {
                BirthDate = new DateTime(year, month, day)
            };

            //Act

            //Assert
            Assert.IsFalse(customer.IsAdult());
        }
    }
}
