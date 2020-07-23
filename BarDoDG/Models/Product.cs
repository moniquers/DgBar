using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Models
{
    public class Product: BaseModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
