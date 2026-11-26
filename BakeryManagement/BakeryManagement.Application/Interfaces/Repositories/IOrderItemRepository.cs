using BakeryManagement.Domain.Entities;

namespace BakeryManagement.Application.Interfaces.Repositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<bool> ExistsByProductIdAsync(Guid productId);
    }
}
