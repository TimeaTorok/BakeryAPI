using BakeryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakeryManagement.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(table =>
            {
                table.HasCheckConstraint(
                    "CK_Products_Category",
                    "[Category] IN ('Bread', 'Pastry', 'Cake', 'Cookie', 'Muffin', 'Other')");
                table.HasCheckConstraint("CK_Products_Price_NonNegative", "[Price] >= 0");
            });

            builder.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(product => product.Description)
                .HasMaxLength(1000);

            builder.Property(product => product.Category)
                .HasConversion<string>();

            builder.Property(product => product.Price)
                .HasPrecision(10, 2);
        }
    }
}
