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
        [TestCase(-10)]
        public void CreateTransaction_NegativePrice_ShouldThrowException(int price)
        {
            Assert.Throws<InvalidOperationException>(() => Transaction.CreateTransaction(new Item(), new Customer(), 1, price));
        }

        [Test]
        public void CreateTransaction_NullItem_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() => Transaction.CreateTransaction(null, new Customer(), 1, 10.00M));
        }

        [Test]
        public void GetTransactionAmount_GivenPriceAndNegativeQuantity_ShouldReturnPositiveValue()
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

            var transaction = Transaction.CreateTransaction(item, customer, -1, 10.00M);

            var result = transaction.GetTransactionAmount();

            Assert.That(result, Is.EqualTo(10.00M));
        }

        [Test]
        public void GetTransactionAmount_GivenPriceAndPositiveQuantity_ShouldReturnNegativeValue()
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

            Assert.That(result, Is.EqualTo(-10.00M));
        }

        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        public void IsPurchase_PositiveNonNegative_ShouldReturnTrue(int quantity, bool expectedResult)
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

            var transaction = Transaction.CreateTransaction(item, customer, quantity, 10.00M);

            var result = transaction.IsPurchase;

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
