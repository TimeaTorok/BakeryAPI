using BakeryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakeryManagement.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(table =>
            {
                table.HasCheckConstraint("CK_OrderItems_Quantity_Positive", "[Quantity] > 0");
                table.HasCheckConstraint("CK_OrderItems_UnitPrice_NonNegative", "[UnitPriceAtOrder] >= 0");
            });

            builder.HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.Items)
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(orderItem => orderItem.Product)
                .WithMany(product => product.OrderItems)
                .HasForeignKey(orderItem => orderItem.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(orderItem => new { orderItem.OrderId, orderItem.ProductId })
                .IsUnique();

            builder.Property(item => item.ProductNameAtOrder)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(item => item.UnitPriceAtOrder)
                .HasPrecision(10, 2);
        }
    }
}
