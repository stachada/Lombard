using Lombard.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface ITransactionService
    {
        Task BuyAsync(int itemId, int customerId, int quantity, decimal price);
        Task SellAsync(int itemId, int customerId, int quantity, decimal price);
    }
}
