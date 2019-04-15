using Lombard.BL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
