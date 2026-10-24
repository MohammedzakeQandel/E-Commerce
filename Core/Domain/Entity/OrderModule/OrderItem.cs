namespace Domain.Entity.OrderModule
{
    public class OrderItem : BaseEntity<Guid>
    {
        public OrderItem(ProductInOrdarItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public OrderItem()
        {
            
        }
        public ProductInOrdarItem Product {  get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}