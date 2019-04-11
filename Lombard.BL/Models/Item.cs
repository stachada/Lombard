using System;

namespace Lombard.BL.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }


        public void IncreaseItemQuantityByGivenValue(int quantity)
        {
            Quantity += quantity;
        }

        public void DecreaseItemQuantityByGivenValue(int quantity)
        {
            Quantity -= quantity;
        }

        public void ChangeItemName(string name)
        {
            Name = name;
        }

        public decimal CalculateTotalPrice()
        {
            return Quantity * Price;
        }
    }
}
