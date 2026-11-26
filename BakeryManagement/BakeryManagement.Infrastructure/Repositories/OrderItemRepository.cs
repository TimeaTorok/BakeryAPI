using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class OrderItemRepository(BakeryDbContext context)
        : GenericRepository<OrderItem>(context), IOrderItemRepository
    {
        public Task<bool> ExistsByProductIdAsync(Guid productId) =>
            _context.OrderItems.AnyAsync(orderItem => orderItem.ProductId == productId);

        public Task<bool> ExistsByOrderIdAndProductIdAsync(Guid orderId, Guid productId) =>
            _context.OrderItems.AnyAsync(orderItem =>
                orderItem.OrderId == orderId && orderItem.ProductId == productId);

        public async Task AddToOrderAsync(Order order, OrderItem orderItem)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFromOrderAsync(Order order, OrderItem orderItem)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.Entry(orderItem).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInOrderAsync(Order order, OrderItem orderItem)
        {
            _context.Entry(order).State = EntityState.Modified;
            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<OrderItem>> FilterPageAsync(
            int pageNumber,
            int pageSize,
            Guid? orderId)
        {
            var query = _context.OrderItems.AsNoTracking();
            if (orderId.HasValue)
                query = query.Where(orderItem => orderItem.OrderId == orderId.Value);

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(orderItem => orderItem.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<OrderItem>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
    }
}
