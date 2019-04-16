using Lombard.BL.Helpers;

namespace LombardAPI.Queries
{
    public class TransactionByProductCategoryQuery : TransactionsQuery
    {
        public ProductCategory Category { get; set; }
    }
}
