using BakeryManagement.Domain.Entities;

using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Application.Interfaces.Repositories
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<bool> ExistsByProductIdAsync(Guid productId);
        Task<PagedResult<OrderItem>> FilterPageAsync(int pageNumber, int pageSize, Guid? orderId);
        Task<bool> ExistsByOrderIdAndProductIdAsync(Guid orderId, Guid productId);
        Task AddToOrderAsync(Order order, OrderItem orderItem);
        Task UpdateInOrderAsync(Order order, OrderItem orderItem);
        Task DeleteFromOrderAsync(Order order, OrderItem orderItem);
    }
}
