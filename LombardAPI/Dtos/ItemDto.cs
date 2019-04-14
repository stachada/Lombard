using Lombard.BL.Helpers;

namespace LombardAPI.Dtos
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string ProductCategory { get; set; }
    }
}
