using BakeryManagement.Application.DTOs.OrderItems;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Application.Interfaces.Services
{
    public interface IOrderItemService
    {
        Task<PagedResult<OrderItemDto>> GetAllOrderItemsAsync(
            int pageNumber,
            int pageSize,
            Guid? orderId);
        Task<OrderItemDto> GetOrderItemByIdAsync(Guid id);
        Task<OrderItemDto> CreateOrderItemAsync(CreateOrderItemDto request);
        Task<OrderItemDto> UpdateOrderItemAsync(Guid id, UpdateOrderItemDto request);
        Task DeleteOrderItemAsync(Guid id);
    }
}
