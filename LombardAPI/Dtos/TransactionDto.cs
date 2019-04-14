using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LombardAPI.Dtos
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public int ItemId { get; set; }
        public int CustomerId { get; set; }
        public float Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
