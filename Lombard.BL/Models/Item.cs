using Lombard.BL.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lombard.BL.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public virtual void IncreaseItemQuantityByGivenValue(float quantity)
        {
            Quantity += quantity;
        }

        public virtual void DecreaseItemQuantityByGivenValue(float quantity)
        {
            Quantity -= quantity;
        }

        public void ChangeItemName(string name)
        {
            Name = name;
        }

        public void ChangeItemCategory(ProductCategory category)
        {
            ProductCategory = category;
        }

        public decimal CalculateTotalPrice()
        {
            return (decimal)Quantity * Price;
        }
    }
}
