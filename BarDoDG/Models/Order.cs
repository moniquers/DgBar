using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarDoDG.Models
{
    public class Order: BaseModel
    {
        public Order()
        {
        }
        public Order(string code, bool closed)
        {
            Code = code;
            Closed = closed;
        }

        public string Code { get; set; }
        public bool Closed { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
