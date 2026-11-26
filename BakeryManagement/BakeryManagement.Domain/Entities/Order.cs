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

        public bool CanTransitionTo(OrderStatus nextStatus) =>
            Status == nextStatus || Status switch
            {
                OrderStatus.Pending => nextStatus is OrderStatus.Confirmed or OrderStatus.Cancelled,
                OrderStatus.Confirmed => nextStatus is OrderStatus.InPreparation or OrderStatus.Cancelled,
                OrderStatus.InPreparation => nextStatus is OrderStatus.ReadyForPickup or OrderStatus.Cancelled,
                OrderStatus.ReadyForPickup => nextStatus is OrderStatus.Completed or OrderStatus.Cancelled,
                OrderStatus.Completed or OrderStatus.Cancelled => false,
                _ => false
            };
    }
}
