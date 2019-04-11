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
        public void Is_Adult_Test()
        {
            //Prepare
            Customer customer = new Customer()
            {
                BirthDate = new DateTime(1997, 5, 6)
            };

            //Act

            //Assert
            Assert.IsTrue(customer.IsAdult());
        }
    }
}
