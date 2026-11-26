using BakeryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BakeryManagement.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(customer => customer.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(customer => customer.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(customer => customer.Email)
                .IsRequired()
                .HasMaxLength(254);

            builder.Property(customer => customer.PhoneNumber)
                .HasMaxLength(32);

            builder.HasIndex(customer => customer.Email)
                .IsUnique();
        }
    }
}
