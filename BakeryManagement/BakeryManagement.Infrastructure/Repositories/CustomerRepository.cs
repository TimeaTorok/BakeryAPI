using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class CustomerRepository(BakeryDbContext context)
        : GenericRepository<Customer>(context), ICustomerRepository
    {
        public Task<bool> ExistsByEmailAsync(string email, Guid? excludingCustomerId = null) =>
            _context.Customers.AnyAsync(customer =>
                customer.Email == email &&
                (!excludingCustomerId.HasValue || customer.Id != excludingCustomerId.Value));

    }
}
