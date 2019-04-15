using Lombard.BL.Models;
using System;

namespace Lombard.BL.Validators
{
    public static class CustomerValidator
    {
        public static bool ValidateDefault(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
            {
                return false;
            }
            if (DateTime.Compare(customer.BirthDate, DateTime.Now) > 0)
            {
                return false;
            }

            return true;
        }
    }
}
