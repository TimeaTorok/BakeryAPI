using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence.Seeding;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Persistence
{
    public class BakeryDbContext(
        DbContextOptions<BakeryDbContext> options)
        : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(BakeryDbContext).Assembly);

            ProductSeedData.Seed(modelBuilder);
            CustomerSeedData.Seed(modelBuilder);
            OrderSeedData.Seed(modelBuilder);
            OrderItemSeedData.Seed(modelBuilder);
        }
    }
}
