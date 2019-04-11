using System;

namespace Lombard.BL.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public void IncreaseItemQuantity()
        {
            Quantity++;
        }

        public void SetItemName(string name)
        {
            Name = name;
        }
    }
}
