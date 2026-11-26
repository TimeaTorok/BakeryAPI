namespace BakeryManagement.Domain.Entities
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
