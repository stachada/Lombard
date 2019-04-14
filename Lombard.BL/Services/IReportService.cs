using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface IReportService
    {
        Task<decimal> GetTurnover(DateTime start, DateTime end);
        Task<decimal> GetProfit(DateTime start, DateTime end);
    }
}
