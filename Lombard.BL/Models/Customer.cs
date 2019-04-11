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
            Name = newName;          
        }
        
        public bool IsAdult()
        {
            return BirthDate.AddYears(18) < DateTime.Now ? true : false;
        }     
    }
}
