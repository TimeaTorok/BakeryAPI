using BakeryManagement.Domain.Enums;

namespace BakeryManagement.Domain.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
