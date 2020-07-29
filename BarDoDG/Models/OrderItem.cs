namespace BarDoDG.Models
{
    public class OrderItem: BaseModel
    {
        public OrderItem()
        {
        }

        public OrderItem(Product product, Order order)
        {
            Product = product;
            Order = order;
        }

        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Amount { get; set; }
    }
}
