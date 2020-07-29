namespace BarDoDG.ApiModels
{
    public class OrderItemResponse
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}