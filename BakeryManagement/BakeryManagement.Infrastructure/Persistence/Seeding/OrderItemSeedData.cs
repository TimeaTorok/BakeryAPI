using BakeryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Persistence.Seeding
{
    public static class OrderItemSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasData(
                // Order 1 (Ana): 19.50 + 14.50 + 18.00 = 52.00 RON
                new
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000001"),
                    OrderId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ProductNameAtOrder = "Country Sourdough Loaf",
                    UnitPriceAtOrder = 19.50m,
                    Quantity = 1
                },
                new
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000002"),
                    OrderId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                    ProductNameAtOrder = "Pistachio Croissant",
                    UnitPriceAtOrder = 14.50m,
                    Quantity = 1
                },
                new
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000003"),
                    OrderId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                    ProductNameAtOrder = "Red Velvet Cake Slice",
                    UnitPriceAtOrder = 18.00m,
                    Quantity = 1
                },

                // Order 2 (Ana): 9.50 + 9.00 = 18.50 RON
                new
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000004"),
                    OrderId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000062"),
                    ProductNameAtOrder = "Chocolate Muffin",
                    UnitPriceAtOrder = 9.50m,
                    Quantity = 1
                },
                new
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000005"),
                    OrderId = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000076"),
                    ProductNameAtOrder = "Classic Fudge Brownie",
                    UnitPriceAtOrder = 9.00m,
                    Quantity = 1
                },

                // Order 3 (Mihai): 9.00 = 9.00 RON
                new
                {
                    Id = Guid.Parse("30000000-0000-0000-0000-000000000006"),
                    OrderId = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    ProductId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                    ProductNameAtOrder = "Butter Croissant",
                    UnitPriceAtOrder = 9.00m,
                    Quantity = 1
                }
            );
        }
    }
}
