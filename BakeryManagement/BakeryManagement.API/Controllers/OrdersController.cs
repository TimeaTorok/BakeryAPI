using BakeryManagement.Application.Constants;
using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<OrderDto>>> GetAll(
            [FromQuery, Range(PaginationLimits.MinimumPageNumber, PaginationLimits.MaximumPageNumber)]
                int pageNumber = PaginationLimits.DefaultPageNumber,
            [FromQuery, Range(PaginationLimits.MinimumPageSize, PaginationLimits.MaximumPageSize)]
                int pageSize = PaginationLimits.DefaultPageSize,
            [FromQuery] Guid? customerId = null,
            [FromQuery] string? status = null) =>
            Ok(await orderService.GetAllOrdersAsync(pageNumber, pageSize, customerId, status));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderWithItemsDto>> GetById(Guid id) =>
            Ok(await orderService.GetOrderByIdAsync(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto createOrder)
        {
            var order = await orderService.CreateOrderAsync(createOrder);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> Update(Guid id, UpdateOrderDto updateOrder)
        {
            var order = await orderService.UpdateOrderAsync(id, updateOrder);
            return Ok(order);
        }
    }
}
