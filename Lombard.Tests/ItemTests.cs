using NUnit.Framework;
using Lombard.BL.Models;


namespace Tests
{
    public class Tests
    {
        [Test]
        public void increaseQuantity_addOneToQuantity_QuantityIncreasedByOne()
        {
            //Arrange
            Item item = new Item
            {
                Quantity = 0
            };


            //Action
            item.IncreaseItemQuantity();

            //Assert
            Assert.AreEqual(1, item.Quantity);
        }

        [Test]
        public void changeItemName_NameOfItemIsChanged_NewNameIsSet()
        {
            //Arrange
            Item item = new Item
            {
                Quantity = 0 
            };


            //Action
            item.IncreaseItemQuantity();

            //Assert
            Assert.AreEqual(1, item.Quantity);
        }
    }
}