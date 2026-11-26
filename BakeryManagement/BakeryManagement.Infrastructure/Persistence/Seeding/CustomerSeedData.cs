using BakeryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Persistence.Seeding
{
    public static class CustomerSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    FirstName = "Ana",
                    LastName = "Popescu",
                    Email = "ana.popescu@crumbandco.example",
                    PhoneNumber = "+40 700 000 001"
                },
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    FirstName = "Mihai",
                    LastName = "Ionescu",
                    Email = "mihai.ionescu@crumbandco.example",
                    PhoneNumber = "+40 700 000 002"
                },
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                    FirstName = "Elena",
                    LastName = "Marin",
                    Email = "elena.marin@crumbandco.example",
                    PhoneNumber = (string?)null
                },
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                    FirstName = "Andrei",
                    LastName = "Dumitrescu",
                    Email = "andrei.dumitrescu@crumbandco.example",
                    PhoneNumber = "+40 700 000 004"
                },
                new
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                    FirstName = "Ioana",
                    LastName = "Radu",
                    Email = "ioana.radu@crumbandco.example",
                    PhoneNumber = (string?)null
                }
            );
        }
    }
}
