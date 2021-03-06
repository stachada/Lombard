﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Lombard.BL.Models;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}
