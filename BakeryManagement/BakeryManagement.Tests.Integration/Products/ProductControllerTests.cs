using System.Net;
using System.Net.Http.Json;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.DTOs.Products;

namespace BakeryManagement.Tests.Integration.Products;

public class ProductControllerTests : ProductIntegrationTestBase
{
    [Fact]
    public async Task GetProducts_WithFilters_ReturnsMatchingProducts()
    {
        var response = await Client.GetAsync(
            "/api/products?pageNumber=1&pageSize=10&category=Bread&isAvailable=true&name=COUNTRY%20SOURDOUGH");

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PagedResult<ProductDto>>();

        Assert.NotNull(result);
        var product = Assert.Single(result.Items);
        Assert.Equal(SeededCountrySourdoughId, product.Id);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task GetProductById_WhenProductExists_ReturnsProduct()
    {
        var response = await Client.GetAsync($"/api/products/{SeededCountrySourdoughId}");

        response.EnsureSuccessStatusCode();
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        Assert.NotNull(product);
        Assert.Equal(SeededCountrySourdoughId, product.Id);
        Assert.Equal("Country Sourdough Loaf", product.Name);
    }

    [Fact]
    public async Task GetProductById_WhenProductDoesNotExist_ReturnsNotFound()
    {
        var response = await Client.GetAsync($"/api/products/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WhenCategoryIsInvalid_ReturnsBadRequest()
    {
        var request = new CreateProductDto
        {
            Name = "Invalid Product",
            Category = "NotACategory",
            Price = 5.00m,
            IsAvailable = true
        };

        var response = await Client.PostAsJsonAsync("/api/products", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateProduct_WhenRequestIsValid_ReturnsCreatedProduct()
    {
        var request = new CreateProductDto
        {
            Name = "New Test Bread",
            Description = "Created by an integration test",
            Category = "Bread",
            Price = 6.25m,
            IsAvailable = true
        };

        var response = await Client.PostAsJsonAsync("/api/products", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        Assert.NotNull(product);
        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal(request.Name, product.Name);
        Assert.Equal(request.Price, product.Price);
    }

    [Fact]
    public async Task UpdateProduct_WhenProductDoesNotExist_ReturnsNotFound()
    {
        var request = new UpdateProductDto
        {
            Name = "Updated Bread",
            Description = "Updated by an integration test",
            Category = "Bread",
            Price = 7.00m,
            IsAvailable = true
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/products/{Guid.NewGuid()}", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateProduct_WhenProductExists_ReturnsUpdatedProduct()
    {
        var request = new UpdateProductDto
        {
            Name = "Updated Integration Bread",
            Description = "Updated by an integration test",
            Category = "Bread",
            Price = 7.00m,
            IsAvailable = false
        };

        var response = await Client.PutAsJsonAsync(
            $"/api/products/{SeededCountrySourdoughId}", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        Assert.NotNull(product);
        Assert.Equal("Updated Integration Bread", product.Name);
        Assert.Equal(7.00m, product.Price);
        Assert.False(product.IsAvailable);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductDoesNotExist_ReturnsNotFound()
    {
        var response = await Client.DeleteAsync($"/api/products/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductIsNotUsed_ReturnsNoContent()
    {
        var response = await Client.DeleteAsync($"/api/products/{SeededUnreferencedProductId}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteProduct_WhenProductIsInAnOrder_ReturnsConflict()
    {
        var response = await Client.DeleteAsync($"/api/products/{SeededCountrySourdoughId}");

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}
