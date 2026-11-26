using BakeryManagement.Application.DTOs.OrderItems;

namespace BakeryManagement.Application.DTOs.Orders
{
    public class OrderWithItemsDto : OrderDto
    {
        public IReadOnlyList<OrderItemSummaryDto> Items { get; set; } = [];
    }
}
