using System;
using System.Collections.Generic;
using System.Text;
using Lombard.BL.Models;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface IItemsRepository
    {
        void AddItem(Item item);
        void DeleteItem(int itemId);
        void UpdateItem(Item item);
        Item GetItemById(int itemId);
        IEnumerable<Item> GetAll();
    }
}
