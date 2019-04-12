using Lombard.BL.Models;
using NUnit.Framework;
using System;

namespace Lombard.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void CreateTransaction_GivenItemCustomerQuantity_ShouldCreateCorrectCustomer()
        {
            var item = new Item()
            {
                ItemId = 1,
                Price = 10.00M,
                Name = "",
                Quantity = 10,
            };
            var transaction = Transaction.CreateTransaction(item, new Customer(), 1, 10.00M);


            Assert.Multiple(() =>
            {
                Assert.That(transaction.Item, Is.Not.Null);
                Assert.That(transaction.Customer, Is.Not.Null);
                Assert.That(transaction.Quantity, Is.EqualTo(1));
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void CreateTransaction_NegativeQuantity_ShouldThrowException(int quantity)
        {
            Assert.Throws<InvalidOperationException>(() => Transaction.CreateTransaction(new Item(), new Customer(), quantity, 10.00M));
        }

        [Test]
        public void CreateTransaction_NullCustomer_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => Transaction.CreateTransaction(new Item(), null, 1, 10.00M));
        }

        [Test]
        public void CreateTransaction_NullItem_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => Transaction.CreateTransaction(null, new Customer(), 1, 10.00M));
        }

        [Test]
        public void CreateTransaction_QuantityLessThanItemQuantity_ShouldThrowException()
        {
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "Customer1",
                BirthDate = new System.DateTime(1990, 12, 12)
            };

            var item = new Item()
            {
                ItemId = 1,
                Price = 10.00M,
                Name = "",
                Quantity = 10,
            };

            Assert.Throws<InvalidOperationException>(() => Transaction.CreateTransaction(item, customer, 11, 10.00M));
        }

        [Test]
        public void GetTransactionAmount_GivenPriceAndQuantity_ShouldReturnProperValue()
        {
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "Customer1",
                BirthDate = new System.DateTime(1990, 12, 12)
            };

            var item = new Item()
            {
                ItemId = 1,
                Price = 10.00M,
                Name = "",
                Quantity = 10,
            };

            var transaction = Transaction.CreateTransaction(item, customer, 1, 10.00M);

            var result = transaction.GetTransactionAmount();

            Assert.That(10.00M, Is.EqualTo(result));
        }

        [Test]
        public void GetTransactionAmount_GivenAnotherPriceAndQuantity_ShouldReturnProperValue()
        {
            var customer = new Customer()
            {
                CustomerId = 1,
                Name = "Customer1",
                BirthDate = new System.DateTime(1990, 12, 12)
            };

            var item = new Item()
            {
                ItemId = 1,
                Price = 12.00M,
                Name = "",
                Quantity = 10,
            };

            var transaction = Transaction.CreateTransaction(item, customer, 1, 10.00M);

            var result = transaction.GetTransactionAmount();

            Assert.That(12.00M, Is.EqualTo(result));
        }
    }
}
