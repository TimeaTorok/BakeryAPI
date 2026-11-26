namespace BakeryManagement.Domain.Entities
{
    public class OrderItem : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public required string ProductNameAtOrder { get; set; }
        public decimal UnitPriceAtOrder { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
