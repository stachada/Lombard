using System;

namespace LombardAPI.Queries
{
    public class TransactionToDateQuery : TransactionsQuery
    {
        public DateTime Date { get; set; }
    }
}
