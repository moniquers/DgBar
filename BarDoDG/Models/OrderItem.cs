using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Models
{
    public class OrderItem: BaseModel
    {
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
