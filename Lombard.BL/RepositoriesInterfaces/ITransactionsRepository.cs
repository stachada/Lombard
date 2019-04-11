using Lombard.BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ITransactionsRepository
    {
        IEnumerable<Transaction> GetAll();
        Transaction GetById(int id);
        void Add(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(int id);
    }
}
