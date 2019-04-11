using Lombard.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ITransactionsRepository
    {
        IEnumerable<Transaction> GetAll();
        Transaction GetById(int id);
        Task Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(int id);
    }
}
