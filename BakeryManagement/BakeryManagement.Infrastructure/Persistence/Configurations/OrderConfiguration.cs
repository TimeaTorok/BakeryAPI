using BakeryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakeryManagement.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "CK_Orders_Status",
                    "[Status] IN ('Pending', 'Confirmed', 'InPreparation', 'ReadyForPickup', 'Completed', 'Cancelled')");
                table.HasCheckConstraint("CK_Orders_TotalPrice_NonNegative", "[TotalPrice] >= 0");
            });

            builder.HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(order => order.Notes)
                .HasMaxLength(2000);

            builder.Property(order => order.Status)
                .HasConversion<string>();

            builder.Property(order => order.TotalPrice)
                .HasPrecision(10, 2);
        }
    }
}
