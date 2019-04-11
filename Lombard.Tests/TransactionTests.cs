using Lombard.BL.Models;
using NUnit.Framework;

namespace Lombard.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void GetTransactionAmount_GivenPriceAndQuantity_ShouldReturnProperValue()
        {
            var transaction = new Transaction();

            var result = transaction.GetTransactionAmount();
        }
    }
}
