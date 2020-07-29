using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Models
{
    public class Product: BaseModel
    {
        public Product()
        {
        }

        public Product(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
