using NUnit.Framework;
using Lombard.BL.Models;


namespace Lombard.Tests
{
    public class ItemTests
    {
        [TestCase(1,12,13)]
        [TestCase(100, 24, 124)]
        [TestCase(400, 100, 500)]
        public void IncreaseItemQuantityByGivenValue_addToQuantity_QuantityIncreased(int initialQuantity, int addedQuantity, int quantityAfterOperation)
        {
            //Arrange
            Item item = new Item
            {
                Quantity = initialQuantity
            };


            //Action
            item.IncreaseItemQuantityByGivenValue(addedQuantity);

            //Assert
            Assert.AreEqual(quantityAfterOperation, item.Quantity);
        }

        [TestCase(1, 1, 0)]
        [TestCase(100, 24, 76)]
        [TestCase(400, 100, 300)]
        public void DecreaseItemQuantityByGivenValue_decreaseQuantity_QuantityDecreased(int initialQuantity, int decreasedQuantity, int quantityAfterOperation)
        {
            //Arrange
            Item item = new Item
            {
                Quantity = initialQuantity
            };

            //Action
            item.DecreaseItemQuantityByGivenValue(decreasedQuantity);

            //Assert
            Assert.AreEqual(quantityAfterOperation, item.Quantity);
        }

        [Test]
        public void ChangeItemName_NameOfItemIsChanged_NewNameIsSet()
        {
            //Arrange
            Item item = new Item
            {
                Name = "Tire"
            };

            Assert.AreEqual("Tire", item.Name);

            //Action
            item.ChangeItemName("Car");

            //Assert
            Assert.AreEqual("Car", item.Name);
        }

        [TestCase(1,12.5,12.5)]
        [TestCase(2,30.5,61.0)]
        [TestCase(384,175.132,67250.688)]
        public void CalculateTotalPrice_PriceAndQuantity_CorrectPriceIsReturned(int quantity, decimal price, decimal expectedResult)
        {
            //Arrange
            Item item = new Item
            {
                Quantity = quantity,
                Price = price
            };

            //Action
            decimal result = item.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}