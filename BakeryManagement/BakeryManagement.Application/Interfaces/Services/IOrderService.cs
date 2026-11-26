using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<PagedResult<OrderDto>> GetAllOrdersAsync(
            int pageNumber,
            int pageSize,
            Guid? customerId,
            string? status);
        Task<OrderWithItemsDto> GetOrderByIdAsync(Guid id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto request);
        Task<OrderDto> UpdateOrderAsync(Guid id, UpdateOrderDto request);
    }
}
