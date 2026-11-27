using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class CustomerRepository(BakeryDbContext context)
        : GenericRepository<Customer>(context), ICustomerRepository
    {
        public async Task<PagedResult<Customer>> SearchPageAsync(
            int pageNumber, int pageSize, string? name)
        {
            var query = _context.Customers.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchTerm = name.ToLower();
                query = query.Where(customer =>
                    (customer.FirstName + " " + customer.LastName)
                    .ToLower()
                    .Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(customer => customer.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Customer>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public Task<bool> ExistsByEmailAsync(string email, Guid? excludingCustomerId = null) =>
            _context.Customers.AnyAsync(customer =>
                customer.Email == email &&
                (!excludingCustomerId.HasValue || customer.Id != excludingCustomerId.Value));

    }
}
