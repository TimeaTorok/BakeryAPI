using System.Net;
using System.Net.Http.Json;
using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Tests.Integration.Orders;

public class OrderControllerTests : OrderIntegrationTestBase
{
    [Fact]
    public async Task GetOrders_WithCustomerIdFilter_ReturnsMatchingOrders()
    {
        var response = await Client.GetAsync(
            $"/api/orders?pageNumber=1&pageSize=10&customerId={SeededCustomerWithOrdersId}");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<OrderDto>>();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.All(result.Items, order =>
            Assert.Equal(SeededCustomerWithOrdersId, order.CustomerId));
    }

    [Fact]
    public async Task GetOrders_WithStatusFilter_ReturnsMatchingOrders()
    {
        var response = await Client.GetAsync(
            "/api/orders?pageNumber=1&pageSize=10&status=Confirmed");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<OrderDto>>();

        Assert.NotNull(result);
        Assert.Equal(3, result.TotalCount);
        Assert.All(result.Items, order => Assert.Equal("Confirmed", order.Status));
    }

    [Fact]
    public async Task CreateOrder_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        var request = new CreateOrderDto
        {
            CustomerId = Guid.NewGuid(),
            Notes = "Order with an unknown customer"
        };

        var response = await Client.PostAsJsonAsync("/api/orders", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WhenCustomerExists_ReturnsCreatedOrder()
    {
        var request = new CreateOrderDto
        {
            CustomerId = SeededCustomerWithOrdersId,
            Notes = "Created by an integration test"
        };

        var response = await Client.PostAsJsonAsync("/api/orders", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();

        Assert.NotNull(order);
        Assert.NotEqual(Guid.Empty, order.Id);
        Assert.Equal(SeededCustomerWithOrdersId, order.CustomerId);
        Assert.Equal("Pending", order.Status);
        Assert.Equal(request.Notes, order.Notes);
    }

    [Fact]
    public async Task GetOrderById_WhenOrderDoesNotExist_ReturnsNotFound()
    {
        var response = await Client.GetAsync($"/api/orders/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetOrderById_WhenOrderExists_ReturnsOrder()
    {
        var response = await Client.GetAsync($"/api/orders/{SeededOrderId}");

        response.EnsureSuccessStatusCode();
        var order = await response.Content.ReadFromJsonAsync<OrderWithItemsDto>();

        Assert.NotNull(order);
        Assert.Equal(SeededOrderId, order.Id);
        Assert.Equal(SeededCustomerWithOrdersId, order.CustomerId);
        Assert.Equal("Confirmed", order.Status);
    }

    [Fact]
    public async Task UpdateOrder_WhenOrderDoesNotExist_ReturnsNotFound()
    {
        var request = new UpdateOrderDto
        {
            Status = "InPreparation",
            Notes = "Updated by an integration test"
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/orders/{Guid.NewGuid()}", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateOrder_WhenOrderExists_ReturnsUpdatedOrder()
    {
        var request = new UpdateOrderDto
        {
            Status = "InPreparation",
            Notes = "Updated by an integration test"
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/orders/{SeededOrderId}", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderDto>();

        Assert.NotNull(order);
        Assert.Equal(SeededOrderId, order.Id);
        Assert.Equal("InPreparation", order.Status);
        Assert.Equal(request.Notes, order.Notes);
    }
}
