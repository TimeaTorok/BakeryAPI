using AutoMapper;
using BakeryManagement.Application.DTOs.OrderItems;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Mapping;
using BakeryManagement.Application.Services;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BakeryManagement.Tests.Unit.OrderItems
{
    public abstract class OrderItemServiceTestBase
    {
        protected readonly Mock<IOrderItemRepository> OrderItemRepository = new();
        protected readonly Mock<IOrderRepository> OrderRepository = new();
        protected readonly Mock<IProductRepository> ProductRepository = new();

        protected readonly OrderItemService OrderItemServiceUnderTest;

        protected OrderItemServiceTestBase()
        {
            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<OrderItemMappingProfile>(),
                NullLoggerFactory.Instance);

            var mapper = mapperConfiguration.CreateMapper();

            OrderItemServiceUnderTest = new OrderItemService(
                OrderItemRepository.Object,
                OrderRepository.Object,
                ProductRepository.Object,
                mapper,
                NullLogger<OrderItemService>.Instance);
        }

        protected static Order MakeOrder(Guid? id = null)
        {
            return new Order
            {
                Id = id ?? Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Status = OrderStatus.Pending,
                TotalPrice = 20.00m
            };
        }

        protected static Product MakeProduct(Guid? id = null)
        {
            return new Product
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Sourdough",
                Category = ProductCategory.Bread,
                Price = 10.50m,
                IsAvailable = true
            };
        }

        protected static OrderItem MakeOrderItem(
            Guid? id = null,
            Guid? orderId = null,
            Guid? productId = null)
        {
            return new OrderItem
            {
                Id = id ?? Guid.NewGuid(),
                OrderId = orderId ?? Guid.NewGuid(),
                ProductId = productId ?? Guid.NewGuid(),
                ProductNameAtOrder = "Sourdough",
                UnitPriceAtOrder = 10.50m,
                Quantity = 2
            };
        }

        protected static CreateOrderItemDto MakeCreateRequest(Guid orderId, Guid productId)
        {
            return new CreateOrderItemDto
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = 2
            };
        }

        protected static UpdateOrderItemDto MakeUpdateRequest(int quantity = 3)
        {
            return new UpdateOrderItemDto { Quantity = quantity };
        }
    }
}
