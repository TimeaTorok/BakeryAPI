using System.Net;
using System.Net.Http.Json;
using BakeryManagement.Application.DTOs.OrderItems;
using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Tests.Integration.OrderItems;

public class OrderItemControllerTests : OrderItemIntegrationTestBase
{
    private const decimal SeededProductPrice = 19.50m;

    [Fact]
    public async Task GetOrderItems_ReturnsOrderItems()
    {
        var response = await Client.GetAsync("/api/orderitems?pageNumber=1&pageSize=10");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<OrderItemDto>>();

        Assert.NotNull(result);
        Assert.Equal(6, result.TotalCount);
        Assert.Equal(6, result.Items.Count);
    }

    [Fact]
    public async Task GetOrderItem_WhenItemExists_ReturnsOrderItem()
    {
        var order = await CreatePendingOrderAsync();
        var createdOrderItem = await AddOrderItemAsync(order.Id, quantity: 2);

        var response = await Client.GetAsync($"/api/orderitems/{createdOrderItem.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var orderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

        Assert.NotNull(orderItem);
        Assert.Equal(createdOrderItem.Id, orderItem.Id);
        Assert.Equal(order.Id, orderItem.OrderId);
        Assert.Equal(2, orderItem.Quantity);
    }

    [Fact]
    public async Task GetOrderItem_WhenItemDoesNotExist_ReturnsNotFound()
    {
        var response = await Client.GetAsync($"/api/orderitems/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrderItem_WhenOrderDoesNotExist_ReturnsNotFound()
    {
        var request = new CreateOrderItemDto
        {
            OrderId = Guid.NewGuid(),
            ProductId = SeededProductId,
            Quantity = 1
        };

        var response = await Client.PostAsJsonAsync("/api/orderitems", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrderItem_WhenProductDoesNotExist_ReturnsNotFound()
    {
        var order = await CreatePendingOrderAsync();
        var request = new CreateOrderItemDto
        {
            OrderId = order.Id,
            ProductId = Guid.NewGuid(),
            Quantity = 1
        };

        var response = await Client.PostAsJsonAsync("/api/orderitems", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrderItem_WhenQuantityIsInvalid_ReturnsBadRequest()
    {
        var order = await CreatePendingOrderAsync();
        var request = new CreateOrderItemDto
        {
            OrderId = order.Id,
            ProductId = SeededProductId,
            Quantity = 0
        };

        var response = await Client.PostAsJsonAsync("/api/orderitems", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrderItem_WhenRequestIsValid_CreatesItemAndUpdatesOrderTotal()
    {
        var order = await CreatePendingOrderAsync();
        var request = new CreateOrderItemDto
        {
            OrderId = order.Id,
            ProductId = SeededProductId,
            Quantity = 2
        };

        var response = await Client.PostAsJsonAsync("/api/orderitems", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var orderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

        Assert.NotNull(orderItem);
        Assert.Equal(order.Id, orderItem.OrderId);
        Assert.Equal(SeededProductId, orderItem.ProductId);
        Assert.Equal(2, orderItem.Quantity);
        Assert.Equal(SeededProductPrice, orderItem.UnitPriceAtOrder);
        Assert.Equal(SeededProductPrice * 2, await GetOrderTotalAsync(order.Id));
    }

    [Fact]
    public async Task UpdateOrderItem_WhenQuantityChanges_UpdatesItemAndOrderTotal()
    {
        var order = await CreatePendingOrderAsync();
        var orderItem = await AddOrderItemAsync(order.Id, quantity: 2);
        var request = new UpdateOrderItemDto { Quantity = 3 };

        var response = await Client.PutAsJsonAsync(
            $"/api/orderitems/{orderItem.Id}", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updatedOrderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

        Assert.NotNull(updatedOrderItem);
        Assert.Equal(3, updatedOrderItem.Quantity);
        Assert.Equal(SeededProductPrice * 3, await GetOrderTotalAsync(order.Id));
    }

    [Fact]
    public async Task DeleteOrderItem_WhenItemExists_DeletesItemAndUpdatesOrderTotal()
    {
        var order = await CreatePendingOrderAsync();
        var orderItem = await AddOrderItemAsync(order.Id, quantity: 2);

        var response = await Client.DeleteAsync($"/api/orderitems/{orderItem.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(0m, await GetOrderTotalAsync(order.Id));
    }

    private async Task<OrderDto> CreatePendingOrderAsync()
    {
        var response = await Client.PostAsJsonAsync("/api/orders", new CreateOrderDto
        {
            CustomerId = SeededCustomerId,
            Notes = "Created by an order item integration test"
        });

        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();

        Assert.NotNull(order);
        return order;
    }

    private async Task<OrderItemDto> AddOrderItemAsync(Guid orderId, int quantity)
    {
        var response = await Client.PostAsJsonAsync("/api/orderitems", new CreateOrderItemDto
        {
            OrderId = orderId,
            ProductId = SeededProductId,
            Quantity = quantity
        });

        response.EnsureSuccessStatusCode();
        var orderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

        Assert.NotNull(orderItem);
        return orderItem;
    }

    private async Task<decimal> GetOrderTotalAsync(Guid orderId)
    {
        var response = await Client.GetAsync($"/api/orders/{orderId}");

        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderWithItemsDto>();

        Assert.NotNull(order);
        return order.TotalPrice;
    }
}
