using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lombard.DAL.Repositories
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly DatabaseContext _context;

        public TransactionsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Transaction GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
