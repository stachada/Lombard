﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.BL.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }


        public virtual void IncreaseItemQuantityByGivenValue(int quantity)
        {
            Quantity += quantity;
        }

        public virtual void DecreaseItemQuantityByGivenValue(int quantity)
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
