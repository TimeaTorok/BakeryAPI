using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Domain.Enums;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class OrderRepository(BakeryDbContext context)
        : GenericRepository<Order>(context), IOrderRepository
    {
        public Task<bool> ExistsByCustomerIdAsync(Guid customerId) =>
            _context.Orders.AnyAsync(order => order.CustomerId == customerId);

        public Task<Order?> GetByIdWithItemsAsync(Guid id) =>
            _context.Orders
                .AsNoTracking()
                .Include(order => order.Items)
                .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<PagedResult<Order>> FilterPageAsync(
            int pageNumber,
            int pageSize,
            Guid? customerId,
            OrderStatus? status)
        {
            var query = _context.Orders.AsNoTracking();
            if (customerId.HasValue)
                query = query.Where(order => order.CustomerId == customerId.Value);
            if (status.HasValue)
                query = query.Where(order => order.Status == status.Value);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(order => order.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Order>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

    }
}
