using Lombard.BL.Models;
using NUnit.Framework;

namespace Lombard.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void CreateTransaction_GivenItemCustomerQuantity_ShouldCreateCorrectCustomer()
        {
            var transaction = Transaction.CreateTransaction(new Item(), new Customer(), 1);


            Assert.Multiple(() =>
            {
                Assert.That(transaction.Item, Is.Not.Null);
                Assert.That(transaction.Customer, Is.Not.Null);
                Assert.That(transaction.Quantity, Is.EqualTo(1));
            });
        }
        
        //[Test]
        //public void GetTransactionAmount_GivenPriceAndQuantity_ShouldReturnProperValue()
        //{
        //    var transaction = new Transaction();

        //    var result = transaction.GetTransactionAmount();
        //}
    }
}
