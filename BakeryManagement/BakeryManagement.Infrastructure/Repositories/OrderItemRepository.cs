using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class OrderItemRepository(BakeryDbContext context)
        : GenericRepository<OrderItem>(context), IOrderItemRepository
    {
        public Task<bool> ExistsByProductIdAsync(Guid productId) =>
            _context.OrderItems.AnyAsync(orderItem => orderItem.ProductId == productId);
    }
}
