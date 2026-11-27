using System.Net;
using System.Net.Http.Json;
using BakeryManagement.Application.DTOs.Customers;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Tests.Integration.Customers;

public class CustomerControllerTests : CustomerIntegrationTestBase
{
    [Fact]
    public async Task GetCustomers_WithNameFilter_ReturnsMatchingCustomers()
    {
        var response = await Client.GetAsync(
            "/api/customers?pageNumber=1&pageSize=10&name=ANA");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<CustomerDto>>();

        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Collection(
            result.Items,
            customer => Assert.Equal(SeededCustomerWithOrdersId, customer.Id),
            customer => Assert.Equal(SeededIoanaCustomerId, customer.Id));
    }

    [Fact]
    public async Task GetCustomerById_WhenCustomerExists_ReturnsCustomer()
    {
        var response = await Client.GetAsync($"/api/customers/{SeededCustomerWithOrdersId}");

        response.EnsureSuccessStatusCode();
        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

        Assert.NotNull(customer);
        Assert.Equal(SeededCustomerWithOrdersId, customer.Id);
        Assert.Equal("Ana", customer.FirstName);
        Assert.Equal("Popescu", customer.LastName);
    }

    [Fact]
    public async Task GetCustomerById_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        var response = await Client.GetAsync($"/api/customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WhenEmailIsInvalid_ReturnsBadRequest()
    {
        var request = new CreateCustomerDto
        {
            FirstName = "Invalid",
            LastName = "Customer",
            Email = "not-an-email"
        };

        var response = await Client.PostAsJsonAsync("/api/customers", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_WhenRequestIsValid_ReturnsCreatedCustomer()
    {
        var request = new CreateCustomerDto
        {
            FirstName = "New",
            LastName = "Customer",
            Email = "new.customer@example.com",
            PhoneNumber = "+40 700 123 456"
        };

        var response = await Client.PostAsJsonAsync("/api/customers", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

        Assert.NotNull(customer);
        Assert.NotEqual(Guid.Empty, customer.Id);
        Assert.Equal(request.FirstName, customer.FirstName);
        Assert.Equal(request.LastName, customer.LastName);
        Assert.Equal(request.Email, customer.Email);
    }

    [Fact]
    public async Task UpdateCustomer_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        var request = new UpdateCustomerDto
        {
            FirstName = "Updated",
            LastName = "Customer",
            Email = "updated.customer@example.com"
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/customers/{Guid.NewGuid()}", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_WhenCustomerExists_ReturnsUpdatedCustomer()
    {
        var request = new UpdateCustomerDto
        {
            FirstName = "Ana Updated",
            LastName = "Popescu Updated",
            Email = "ana.updated@example.com",
            PhoneNumber = "+40 700 999 999"
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/customers/{SeededCustomerWithOrdersId}", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customer = await response.Content.ReadFromJsonAsync<CustomerDto>();

        Assert.NotNull(customer);
        Assert.Equal(request.FirstName, customer.FirstName);
        Assert.Equal(request.LastName, customer.LastName);
        Assert.Equal(request.Email, customer.Email);
        Assert.Equal(request.PhoneNumber, customer.PhoneNumber);
    }

    [Fact]
    public async Task DeleteCustomer_WhenCustomerDoesNotExist_ReturnsNotFound()
    {
        var response = await Client.DeleteAsync($"/api/customers/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WhenCustomerHasNoOrders_ReturnsNoContent()
    {
        var response = await Client.DeleteAsync($"/api/customers/{SeededCustomerWithoutOrdersId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCustomer_WhenCustomerHasOrders_ReturnsConflict()
    {
        var response = await Client.DeleteAsync($"/api/customers/{SeededCustomerWithOrdersId}");

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}
