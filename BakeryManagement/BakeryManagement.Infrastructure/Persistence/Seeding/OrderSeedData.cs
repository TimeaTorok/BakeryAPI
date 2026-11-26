using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Persistence.Seeding
{
    public static class OrderSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    CustomerId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Status = OrderStatus.Confirmed,
                    Notes = "A selection of sourdough, pastry, and cake.",
                    CreatedAt = new DateTimeOffset(2026, 11, 20, 9, 0, 0, TimeSpan.Zero),
                    UpdatedAt = (DateTimeOffset?)null,
                    TotalPrice = 52.00m
                },
                new
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    CustomerId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    Status = OrderStatus.Confirmed,
                    Notes = "Muffin and brownie.",
                    CreatedAt = new DateTimeOffset(2026, 11, 21, 10, 30, 0, TimeSpan.Zero),
                    UpdatedAt = (DateTimeOffset?)null,
                    TotalPrice = 18.50m
                },
                new
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    CustomerId = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    Status = OrderStatus.Confirmed,
                    Notes = (string?)null,
                    CreatedAt = new DateTimeOffset(2026, 11, 22, 8, 15, 0, TimeSpan.Zero),
                    UpdatedAt = (DateTimeOffset?)null,
                    TotalPrice = 9.00m
                }
            );
        }
    }
}
