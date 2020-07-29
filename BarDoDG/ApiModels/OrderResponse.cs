using System.Collections.Generic;

namespace BarDoDG.ApiModels
{
    public class OrderResponse
    {

        public int Id { get; set; }
        public string Code { get; set; }
        public IEnumerable<OrderItemResponse> Items { get; set; }
        public bool Closed { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
