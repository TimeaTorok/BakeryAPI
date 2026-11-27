using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Moq;

namespace BakeryManagement.Tests.Unit.Orders
{
    public class OrderServiceTests : OrderServiceTestBase
    {
        [Fact]
        public async Task GetAllOrdersAsync_WhenNoOrdersExist_ReturnsEmptyPage()
        {
            OrderRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, null, null))
                .ReturnsAsync(new PagedResult<Order>
                {
                    Items = [],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 0
                });

            var result = await OrderServiceUnderTest.GetAllOrdersAsync(1, 10, null, null);

            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Equal(0, result.TotalPages);
        }

        [Fact]
        public async Task GetAllOrdersAsync_WhenOrdersExist_ReturnsOrders()
        {
            var order = MakeOrder();

            OrderRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, null, null))
                .ReturnsAsync(new PagedResult<Order>
                {
                    Items = [order],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 1
                });

            var result = await OrderServiceUnderTest.GetAllOrdersAsync(1, 10, null, null);

            Assert.Single(result.Items);
            Assert.Equal(order.Id, result.Items[0].Id);
            Assert.Equal(order.CustomerId, result.Items[0].CustomerId);
            Assert.Equal("Pending", result.Items[0].Status);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetOrderByIdAsync_WhenOrderDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            OrderRepository
                .Setup(repository => repository.GetByIdWithItemsAsync(id))
                .ReturnsAsync((Order?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => OrderServiceUnderTest.GetOrderByIdAsync(id));
        }

        [Fact]
        public async Task GetOrderByIdAsync_WhenOrderExists_ReturnsOrder()
        {
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            var order = MakeOrder(id, customerId);

            OrderRepository
                .Setup(repository => repository.GetByIdWithItemsAsync(id))
                .ReturnsAsync(order);

            var result = await OrderServiceUnderTest.GetOrderByIdAsync(id);

            Assert.Equal(id, result.Id);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal("Pending", result.Status);
            Assert.Equal("Test order", result.Notes);
        }

        [Fact]
        public async Task CreateOrderAsync_WhenCustomerDoesNotExist_ThrowsNotFound()
        {
            var customerId = Guid.NewGuid();
            var request = MakeCreateRequest(customerId);

            CustomerRepository
                .Setup(repository => repository.GetAsync(customerId))
                .ReturnsAsync((Customer?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => OrderServiceUnderTest.CreateOrderAsync(request));

            OrderRepository.Verify(
                repository => repository.AddAsync(It.IsAny<Order>()),
                Times.Never);
        }

        [Fact]
        public async Task CreateOrderAsync_WhenCustomerExists_CreatesOrder()
        {
            var customer = MakeCustomer();
            var request = MakeCreateRequest(customer.Id);

            CustomerRepository
                .Setup(repository => repository.GetAsync(customer.Id))
                .ReturnsAsync(customer);
            OrderRepository
                .Setup(repository => repository.AddAsync(It.IsAny<Order>()))
                .ReturnsAsync((Order order) => order);

            var result = await OrderServiceUnderTest.CreateOrderAsync(request);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(customer.Id, result.CustomerId);
            Assert.Equal("Pending", result.Status);
            Assert.Equal(request.Notes, result.Notes);
            Assert.Equal(0, result.TotalPrice);

            OrderRepository.Verify(
                repository => repository.AddAsync(
                    It.Is<Order>(order => order.CustomerId == customer.Id)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateOrderAsync_WhenStatusChangeIsInvalid_ThrowsConflict()
        {
            var id = Guid.NewGuid();
            var order = MakeOrder(id, status: OrderStatus.Cancelled);

            OrderRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(order);

            await Assert.ThrowsAsync<ConflictException>(
                () => OrderServiceUnderTest.UpdateOrderAsync(id, MakeUpdateRequest("Completed")));

            OrderRepository.Verify(
                repository => repository.UpdateAsync(It.IsAny<Order>()),
                Times.Never);
        }

        [Fact]
        public async Task UpdateOrderAsync_WhenStatusChangeIsValid_UpdatesOrder()
        {
            var id = Guid.NewGuid();
            var order = MakeOrder(id, status: OrderStatus.Pending);
            var request = MakeUpdateRequest("Confirmed");

            OrderRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(order);
            OrderRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            var result = await OrderServiceUnderTest.UpdateOrderAsync(id, request);

            Assert.Equal("Confirmed", result.Status);
            Assert.Equal(request.Notes, result.Notes);
            Assert.NotNull(result.UpdatedAt);

            OrderRepository.Verify(
                repository => repository.UpdateAsync(
                    It.Is<Order>(updated => updated.Id == id)),
                Times.Once);
        }
    }
}
