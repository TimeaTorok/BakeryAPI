using BakeryManagement.Domain.Enums;

namespace BakeryManagement.Domain.Entities
{
    public class Order : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string? Notes { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public decimal TotalPrice { get; set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> Items { get; } = new List<OrderItem>();
    }
}
