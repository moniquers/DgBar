using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Models
{
    public class Order: BaseModel
    {
        public string Status { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
