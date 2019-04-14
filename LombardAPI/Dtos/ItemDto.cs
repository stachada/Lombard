using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LombardAPI.Dtos
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
