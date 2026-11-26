using BakeryManagement.Domain.Entities;

using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Domain.Enums;

namespace BakeryManagement.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<bool> ExistsByCustomerIdAsync(Guid customerId);
        Task<PagedResult<Order>> FilterPageAsync(
            int pageNumber,
            int pageSize,
            Guid? customerId,
            OrderStatus? status);
        Task<Order?> GetByIdWithItemsAsync(Guid id);
    }
}
