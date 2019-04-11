using System;
using System.Collections.Generic;
using System.Text;

namespace Lombard.BL.Models
{
    public class Transaction
    {
        public DateTime TransactionDate { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        //public Item Item { get; private set; }

        public decimal GetTransactionAmount()
        {
            throw new NotImplementedException();
        }

    }
}
