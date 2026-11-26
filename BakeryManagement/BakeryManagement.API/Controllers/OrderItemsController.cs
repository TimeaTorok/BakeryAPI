using BakeryManagement.Application.Constants;
using BakeryManagement.Application.DTOs.OrderItems;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.API.Controllers
{
    [ApiController]
    [Route("api/orderitems")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class OrderItemsController(IOrderItemService orderItemService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<OrderItemDto>>> GetAll(
            [FromQuery, Range(PaginationLimits.MinimumPageNumber, PaginationLimits.MaximumPageNumber)]
                int pageNumber = PaginationLimits.DefaultPageNumber,
            [FromQuery, Range(PaginationLimits.MinimumPageSize, PaginationLimits.MaximumPageSize)]
                int pageSize = PaginationLimits.DefaultPageSize,
            [FromQuery] Guid? orderId = null) =>
            Ok(await orderItemService.GetAllOrderItemsAsync(pageNumber, pageSize, orderId));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<OrderItemDto>> Create(CreateOrderItemDto createOrderItem)
        {
            var orderItem = await orderItemService.CreateOrderItemAsync(createOrderItem);
            return StatusCode(StatusCodes.Status201Created, orderItem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<OrderItemDto>> Update(
            Guid id,
            UpdateOrderItemDto updateOrderItem)
        {
            var orderItem = await orderItemService.UpdateOrderItemAsync(id, updateOrderItem);
            return Ok(orderItem);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await orderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }
}
