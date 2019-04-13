using System;
using System.Collections.Generic;
using System.Text;
using Lombard.BL.Models;

namespace Lombard.BL.RepositoriesInterfaces
{
    public interface ICustomersRepository
    {
        IEnumerable<Customer> GetAll();
        Customer GetCustomerById(int customerId);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }
}
