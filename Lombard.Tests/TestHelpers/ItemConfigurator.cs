using Lombard.BL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Lombard.Tests.TestHelpers
{
    public class ItemConfigurator
    {
        public static IEnumerable<Item> GenerateItems()
        {
            var item1 = new Item()
            {
                ItemId = 1,
                Price = 25.10M,
                Name = "Tire",
                Quantity = 6,
                ProductCategory = BL.Helpers.ProductCategory.Automotive
            };

            var item2 = new Item()
            {
                ItemId = 2,
                Price = 15.55M,
                Name = "Chicken nuggets",
                Quantity = 10,
                ProductCategory = BL.Helpers.ProductCategory.Food
            };

            var item3 = new Item()
            {
                ItemId = 3,
                Price = 5.35M,
                Name = "Rice bag",
                Quantity = 7,
                ProductCategory = BL.Helpers.ProductCategory.Food
            };

            var items = new List<Item>()
            {
                item1,
                item2,
                item3
            };

            return items;
        }

        public static Mock<DbSet<Item>> CreateDbSetMockForItems(IEnumerable<Item> items)
        {
            var dbSetMock = new Mock<DbSet<Item>>();
            dbSetMock.As<IAsyncEnumerable<Item>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Item>(items.AsQueryable().GetEnumerator()));

            dbSetMock.As<IQueryable<Item>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Item>(items.AsQueryable().Provider));

            dbSetMock.As<IQueryable<Item>>()
                .Setup(m => m.Expression)
                .Returns(items.AsQueryable().Expression);

            dbSetMock.As<IQueryable<Item>>()
                .Setup(m => m.ElementType)
                .Returns(items.AsQueryable().ElementType);

            dbSetMock.As<IQueryable<Item>>()
                .Setup(m => m.GetEnumerator())
                .Returns(items.AsQueryable().GetEnumerator());
            return dbSetMock;
        }
    }
}