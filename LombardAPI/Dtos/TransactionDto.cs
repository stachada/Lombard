using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LombardAPI.Dtos
{
    public class TransactionDto
    {
        public int ItemId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
