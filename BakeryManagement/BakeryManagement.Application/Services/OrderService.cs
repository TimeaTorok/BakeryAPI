using System.ComponentModel.DataAnnotations;
using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Interfaces.Services;
using BakeryManagement.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BakeryManagement.Domain.Enums;

namespace BakeryManagement.Application.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IMapper mapper,
        ILogger<OrderService> logger) : IOrderService
    {
        public async Task<PagedResult<OrderDto>> GetAllOrdersAsync(
            int pageNumber,
            int pageSize,
            Guid? customerId,
            string? status)
        {
            PaginationValidator.Validate(pageNumber, pageSize);

            OrderStatus? statusFilter = string.IsNullOrWhiteSpace(status)
                ? null
                : ParseStatus(status);

            var page = await orderRepository.FilterPageAsync(
                pageNumber, pageSize, customerId, statusFilter);

            logger.LogInformation(
                "Retrieved orders page {PageNumber} with page size {PageSize}, customer {CustomerId}, status {Status}; returned {ResultCount} of {TotalCount} orders",
                page.PageNumber, page.PageSize, customerId, status, page.Items.Count, page.TotalCount);

            return new PagedResult<OrderDto>
            {
                Items = mapper.Map<List<OrderDto>>(page.Items),
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                TotalCount = page.TotalCount
            };
        }

        public async Task<OrderWithItemsDto> GetOrderByIdAsync(Guid id)
        {
            var order = await orderRepository.GetByIdWithItemsAsync(id)
                ?? throw new EntityNotFoundException($"Order '{id}' was not found.");

            logger.LogInformation("Retrieved order {OrderId}", id);
            return mapper.Map<OrderWithItemsDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto request)
        {
            if (request.CustomerId == Guid.Empty)
                throw new ValidationException("Customer ID is required.");

            if (await customerRepository.GetAsync(request.CustomerId) is null)
                throw new EntityNotFoundException(
                    $"Customer '{request.CustomerId}' was not found.");

            var order = new Order
            {
                CustomerId = request.CustomerId,
                Notes = request.Notes,
                Status = OrderStatus.Pending,
                TotalPrice = 0,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = null
            };

            await orderRepository.AddAsync(order);
            logger.LogInformation("Created order {OrderId}", order.Id);
            return mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> UpdateOrderAsync(Guid id, UpdateOrderDto request)
        {
            var order = await orderRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Order '{id}' was not found.");

            var nextStatus = ParseStatus(request.Status);
            if (!order.CanTransitionTo(nextStatus))
                throw new ConflictException(
                    $"Order status cannot change from {order.Status} to {nextStatus}.");

            order.Status = nextStatus;
            if (request.Notes is not null)
                order.Notes = request.Notes;
            order.UpdatedAt = DateTimeOffset.UtcNow;

            await orderRepository.UpdateAsync(order);
            logger.LogInformation("Updated order {OrderId}", id);
            return mapper.Map<OrderDto>(order);
        }

        private static OrderStatus ParseStatus(string status)
        {
            var enumName = Enum.GetNames<OrderStatus>()
                .FirstOrDefault(name => string.Equals(
                    name,
                    status.Trim(),
                    StringComparison.OrdinalIgnoreCase));

            if (enumName is null)
                throw new ValidationException(
                    "Order status must be Pending, Confirmed, InPreparation, ReadyForPickup, Completed, or Cancelled.");

            return Enum.Parse<OrderStatus>(enumName);
        }
    }
}
