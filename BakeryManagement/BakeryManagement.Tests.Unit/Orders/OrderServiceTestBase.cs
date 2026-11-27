using AutoMapper;
using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Mapping;
using BakeryManagement.Application.Services;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BakeryManagement.Tests.Unit.Orders
{
    public abstract class OrderServiceTestBase
    {
        protected readonly Mock<IOrderRepository> OrderRepository = new();
        protected readonly Mock<ICustomerRepository> CustomerRepository = new();

        protected readonly OrderService OrderServiceUnderTest;

        protected OrderServiceTestBase()
        {
            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<OrderMappingProfile>(),
                NullLoggerFactory.Instance);

            var mapper = mapperConfiguration.CreateMapper();

            OrderServiceUnderTest = new OrderService(
                OrderRepository.Object,
                CustomerRepository.Object,
                mapper,
                NullLogger<OrderService>.Instance);
        }

        protected static Order MakeOrder(
            Guid? id = null,
            Guid? customerId = null,
            OrderStatus status = OrderStatus.Pending)
        {
            return new Order
            {
                Id = id ?? Guid.NewGuid(),
                CustomerId = customerId ?? Guid.NewGuid(),
                Status = status,
                Notes = "Test order",
                TotalPrice = 20.00m
            };
        }

        protected static Customer MakeCustomer(Guid? id = null)
        {
            return new Customer
            {
                Id = id ?? Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com"
            };
        }

        protected static CreateOrderDto MakeCreateRequest(Guid customerId)
        {
            return new CreateOrderDto
            {
                CustomerId = customerId,
                Notes = "Please call on arrival"
            };
        }

        protected static UpdateOrderDto MakeUpdateRequest(
            string status = "Confirmed")
        {
            return new UpdateOrderDto
            {
                Status = status,
                Notes = "Updated order"
            };
        }
    }
}
