using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Lombard.DAL;

namespace Lombard.Tests
{
    class ItemsRepositoryTests
    {
        [Test]
        public void AddItemToRepository_ItemIsAdded_CountOfItemsIncreasedByOne()
        {
            //Arrange
            var databaseContext = new Mock<DatabaseContext>();

            //Action
           
            //Assert
        }
    }
}
