using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Domain.Entities;
using Moq;

namespace BakeryManagement.Tests.Unit.OrderItems
{
    public class OrderItemServiceTests : OrderItemServiceTestBase
    {
        [Fact]
        public async Task GetAllOrderItemsAsync_WhenNoItemsExist_ReturnsEmptyPage()
        {
            OrderItemRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, null))
                .ReturnsAsync(new PagedResult<OrderItem>
                {
                    Items = [],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 0
                });

            var result = await OrderItemServiceUnderTest.GetAllOrderItemsAsync(1, 10, null);

            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Equal(0, result.TotalPages);
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_WhenItemsExist_ReturnsItems()
        {
            var orderItem = MakeOrderItem();

            OrderItemRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, null))
                .ReturnsAsync(new PagedResult<OrderItem>
                {
                    Items = [orderItem],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 1
                });

            var result = await OrderItemServiceUnderTest.GetAllOrderItemsAsync(1, 10, null);

            Assert.Single(result.Items);
            Assert.Equal(orderItem.Id, result.Items[0].Id);
            Assert.Equal("Sourdough", result.Items[0].ProductNameAtOrder);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_WhenOrderIdIsProvided_FiltersByOrder()
        {
            var orderId = Guid.NewGuid();
            var orderItem = MakeOrderItem(orderId: orderId);

            OrderItemRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, orderId))
                .ReturnsAsync(new PagedResult<OrderItem>
                {
                    Items = [orderItem],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 1
                });

            var result = await OrderItemServiceUnderTest.GetAllOrderItemsAsync(1, 10, orderId);

            Assert.Single(result.Items);
            Assert.Equal(orderId, result.Items[0].OrderId);
            OrderItemRepository.Verify(
                repository => repository.FilterPageAsync(1, 10, orderId),
                Times.Once);
        }

        [Fact]
        public async Task CreateOrderItemAsync_WhenOrderDoesNotExist_ThrowsNotFound()
        {
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var request = MakeCreateRequest(orderId, productId);

            OrderRepository
                .Setup(repository => repository.GetAsync(orderId))
                .ReturnsAsync((Order?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => OrderItemServiceUnderTest.CreateOrderItemAsync(request));

            OrderItemRepository.Verify(
                repository => repository.AddToOrderAsync(
                    It.IsAny<Order>(), It.IsAny<OrderItem>()),
                Times.Never);
        }

        [Fact]
        public async Task CreateOrderItemAsync_WhenRequestIsValid_CreatesOrderItem()
        {
            var order = MakeOrder();
            var product = MakeProduct();
            var request = MakeCreateRequest(order.Id, product.Id);

            OrderRepository
                .Setup(repository => repository.GetAsync(order.Id))
                .ReturnsAsync(order);
            ProductRepository
                .Setup(repository => repository.GetAsync(product.Id))
                .ReturnsAsync(product);
            OrderItemRepository
                .Setup(repository => repository.ExistsByOrderIdAndProductIdAsync(order.Id, product.Id))
                .ReturnsAsync(false);
            OrderItemRepository
                .Setup(repository => repository.AddToOrderAsync(
                    It.IsAny<Order>(), It.IsAny<OrderItem>()))
                .Returns(Task.CompletedTask);

            var result = await OrderItemServiceUnderTest.CreateOrderItemAsync(request);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(order.Id, result.OrderId);
            Assert.Equal(product.Id, result.ProductId);
            Assert.Equal("Sourdough", result.ProductNameAtOrder);
            Assert.Equal(product.Price, result.UnitPriceAtOrder);
            Assert.Equal(2, result.Quantity);

            OrderItemRepository.Verify(
                repository => repository.AddToOrderAsync(
                    order,
                    It.Is<OrderItem>(item =>
                        item.ProductId == product.Id && item.Quantity == request.Quantity)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateOrderItemAsync_WhenOrderItemExists_UpdatesOrderItem()
        {
            var orderItem = MakeOrderItem();
            var order = MakeOrder(orderItem.OrderId);
            order.TotalPrice = orderItem.UnitPriceAtOrder * orderItem.Quantity;

            OrderItemRepository
                .Setup(repository => repository.GetAsync(orderItem.Id))
                .ReturnsAsync(orderItem);
            OrderRepository
                .Setup(repository => repository.GetAsync(order.Id))
                .ReturnsAsync(order);
            OrderItemRepository
                .Setup(repository => repository.UpdateInOrderAsync(
                    It.IsAny<Order>(), It.IsAny<OrderItem>()))
                .Returns(Task.CompletedTask);

            var result = await OrderItemServiceUnderTest.UpdateOrderItemAsync(
                orderItem.Id, MakeUpdateRequest());

            Assert.Equal(orderItem.Id, result.Id);
            Assert.Equal(3, result.Quantity);
            Assert.Equal(31.50m, order.TotalPrice);
            OrderItemRepository.Verify(
                repository => repository.UpdateInOrderAsync(
                    order,
                    It.Is<OrderItem>(updated => updated.Id == orderItem.Id)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateOrderItemAsync_WhenOrderItemDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            OrderItemRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((OrderItem?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => OrderItemServiceUnderTest.UpdateOrderItemAsync(id, MakeUpdateRequest()));
        }

        [Fact]
        public async Task DeleteOrderItemAsync_WhenOrderItemExists_DeletesOrderItem()
        {
            var orderItem = MakeOrderItem();
            var order = MakeOrder(orderItem.OrderId);
            order.TotalPrice = orderItem.UnitPriceAtOrder * orderItem.Quantity;

            OrderItemRepository
                .Setup(repository => repository.GetAsync(orderItem.Id))
                .ReturnsAsync(orderItem);
            OrderRepository
                .Setup(repository => repository.GetAsync(order.Id))
                .ReturnsAsync(order);
            OrderItemRepository
                .Setup(repository => repository.DeleteFromOrderAsync(
                    It.IsAny<Order>(), It.IsAny<OrderItem>()))
                .Returns(Task.CompletedTask);

            await OrderItemServiceUnderTest.DeleteOrderItemAsync(orderItem.Id);

            Assert.Equal(0, order.TotalPrice);
            OrderItemRepository.Verify(
                repository => repository.DeleteFromOrderAsync(
                    order,
                    It.Is<OrderItem>(deleted => deleted.Id == orderItem.Id)),
                Times.Once);
        }

        [Fact]
        public async Task DeleteOrderItemAsync_WhenOrderItemDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            OrderItemRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((OrderItem?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => OrderItemServiceUnderTest.DeleteOrderItemAsync(id));
        }
    }
}
