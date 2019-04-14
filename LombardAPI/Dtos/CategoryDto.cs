using Lombard.BL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LombardAPI.Dtos
{
    public class CategoryDto
    {
        public ProductCategory ProductCategory { get; set; }
        public float Quantity { get; set; }
    }
}
