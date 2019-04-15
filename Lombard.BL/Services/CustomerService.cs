using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Validators;

namespace Lombard.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomersRepository _customerRepository;
        

        public CustomerService(ICustomersRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task CreateNewCustomerAsync(Customer customer)
        {
            if (!CustomerValidator.ValidateDefault(customer))
            {
                throw new InvalidOperationException("Customer's validation failed");
            }

            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            await _customerRepository.DeleteCustomerAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetCustomerByIdAsync(customerId);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            if (!CustomerValidator.ValidateDefault(customer))
            {
                throw new InvalidOperationException("Customer's validation failed");
            }

            await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
