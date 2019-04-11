using System;
using System.Collections.Generic;
using System.Text;

namespace Lombard.BL.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public void ChangeName(string newName)
        {
            throw new NotImplementedException();
        }

        public bool IsAdult()
        {
            throw new NotImplementedException();
        }
    }
}
