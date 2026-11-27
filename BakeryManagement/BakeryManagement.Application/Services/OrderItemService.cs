using System.ComponentModel.DataAnnotations;
using BakeryManagement.Application.DTOs.OrderItems;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Interfaces.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;

namespace BakeryManagement.Application.Services
{
    public class OrderItemService(
        IOrderItemRepository orderItemRepository,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper,
        ILogger<OrderItemService> logger) : IOrderItemService
    {
        private const decimal MaximumOrderTotal = 99_999_999.99m;

        public async Task<OrderItemDto> GetOrderItemByIdAsync(Guid id)
        {
            var orderItem = await orderItemRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Order item '{id}' was not found.");

            return mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task<PagedResult<OrderItemDto>> GetAllOrderItemsAsync(
            int pageNumber,
            int pageSize,
            Guid? orderId)
        {
            PaginationValidator.Validate(pageNumber, pageSize);

            var page = await orderItemRepository.FilterPageAsync(pageNumber, pageSize, orderId);

            logger.LogInformation(
                "Retrieved order items page {PageNumber} with page size {PageSize}, order {OrderId}; returned {ResultCount} of {TotalCount} order items",
                page.PageNumber, page.PageSize, orderId, page.Items.Count, page.TotalCount);

            return new PagedResult<OrderItemDto>
            {
                Items = mapper.Map<List<OrderItemDto>>(page.Items),
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                TotalCount = page.TotalCount
            };
        }

        public async Task<OrderItemDto> CreateOrderItemAsync(CreateOrderItemDto request)
        {
            if (request.OrderId == Guid.Empty)
                throw new ValidationException("Order ID is required.");
            if (request.ProductId == Guid.Empty)
                throw new ValidationException("Product ID is required.");
            if (request.Quantity <= 0)
                throw new ValidationException("Quantity must be greater than zero.");

            var order = await orderRepository.GetAsync(request.OrderId)
                ?? throw new EntityNotFoundException($"Order '{request.OrderId}' was not found.");
            EnsureOrderIsPending(order);

            var product = await productRepository.GetAsync(request.ProductId)
                ?? throw new EntityNotFoundException($"Product '{request.ProductId}' was not found.");

            if (!product.IsAvailable)
                throw new ConflictException("Unavailable products cannot be added to orders.");

            if (await orderItemRepository.ExistsByOrderIdAndProductIdAsync(
                    request.OrderId, request.ProductId))
                throw new ConflictException(
                    "This product already appears in the order.");

            var lineTotal = product.Price * request.Quantity;
            var updatedTotal = order.TotalPrice + lineTotal;
            if (updatedTotal > MaximumOrderTotal)
                throw new ValidationException(
                    $"Order total cannot exceed {MaximumOrderTotal}.");

            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                ProductNameAtOrder = product.Name,
                UnitPriceAtOrder = product.Price,
                Quantity = request.Quantity
            };

            order.TotalPrice = updatedTotal;
            order.UpdatedAt = DateTimeOffset.UtcNow;
            await orderItemRepository.AddToOrderAsync(order, orderItem);

            logger.LogInformation(
                "Created order item {OrderItemId} for order {OrderId}",
                orderItem.Id,
                order.Id);
            return mapper.Map<OrderItemDto>(orderItem);
        }

        public async Task DeleteOrderItemAsync(Guid id)
        {
            var orderItem = await orderItemRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Order item '{id}' was not found.");

            var order = await orderRepository.GetAsync(orderItem.OrderId)
                ?? throw new EntityNotFoundException($"Order '{orderItem.OrderId}' was not found.");
            EnsureOrderIsPending(order);

            order.TotalPrice -= orderItem.UnitPriceAtOrder * orderItem.Quantity;
            order.UpdatedAt = DateTimeOffset.UtcNow;
            await orderItemRepository.DeleteFromOrderAsync(order, orderItem);

            logger.LogInformation(
                "Deleted order item {OrderItemId} from order {OrderId}",
                id,
                order.Id);
        }

        public async Task<OrderItemDto> UpdateOrderItemAsync(
            Guid id,
            UpdateOrderItemDto request)
        {
            if (request.Quantity <= 0)
                throw new ValidationException("Quantity must be greater than zero.");

            var orderItem = await orderItemRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Order item '{id}' was not found.");

            var order = await orderRepository.GetAsync(orderItem.OrderId)
                ?? throw new EntityNotFoundException($"Order '{orderItem.OrderId}' was not found.");
            EnsureOrderIsPending(order);

            var quantityDifference = request.Quantity - orderItem.Quantity;
            var updatedTotal = order.TotalPrice + orderItem.UnitPriceAtOrder * quantityDifference;
            if (updatedTotal < 0 || updatedTotal > MaximumOrderTotal)
                throw new ValidationException(
                    $"Order total must be between 0 and {MaximumOrderTotal}.");

            orderItem.Quantity = request.Quantity;
            order.TotalPrice = updatedTotal;
            order.UpdatedAt = DateTimeOffset.UtcNow;
            await orderItemRepository.UpdateInOrderAsync(order, orderItem);

            logger.LogInformation(
                "Updated order item {OrderItemId} quantity in order {OrderId}",
                id,
                order.Id);
            return mapper.Map<OrderItemDto>(orderItem);
        }

        private static void EnsureOrderIsPending(Order order)
        {
            if (order.Status != OrderStatus.Pending)
                throw new ConflictException(
                    "Order items can only be modified while the order is pending.");
        }
    }
}
