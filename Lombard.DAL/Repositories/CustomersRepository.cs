using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Lombard.DAL.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly DatabaseContext _context;

        public CustomersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }
      
        public async Task DeleteCustomerAsync(int customerId)
        {
            var customerToDelete = await GetCustomerByIdAsync(customerId);

            if(customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Customer does not exist");
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }
      
        public async Task UpdateCustomerAsync(Customer customer)
        {
            var customerToUpdate = await GetCustomerByIdAsync(customer.CustomerId);

            if (customerToUpdate != null)
            {
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Customer does not exist");
            }

        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
    }
}
