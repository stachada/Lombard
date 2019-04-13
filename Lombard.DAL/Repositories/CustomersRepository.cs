using System;
using System.Collections.Generic;
using System.Text;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;

namespace Lombard.DAL.Repositories
{
    class CustomersRepository : ICustomersRepository
    {
        private readonly DatabaseContext _context;

        public CustomersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomerById(int customerId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
