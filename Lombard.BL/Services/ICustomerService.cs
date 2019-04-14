using Lombard.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lombard.BL.Services
{
    public interface ICustomerService
    {
        Task CreateNewCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
        Task UpdateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int customerId);
    }
}
