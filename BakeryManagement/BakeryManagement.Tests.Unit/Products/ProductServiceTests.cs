using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Moq;

namespace BakeryManagement.Tests.Unit.Products
{
    public class ProductServiceTests : ProductServiceTestBase
    {
        [Fact]
        public async Task GetAllProductsAsync_WhenNoProductsExist_ReturnsEmptyPage()
        {
            ProductRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, null, null, null))
                .ReturnsAsync(new PagedResult<Product>
                {
                    Items = [],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 0
                });

            var result = await ProductServiceUnderTest.GetAllProductsAsync(1, 10, null, null, null);

            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Equal(0, result.TotalPages);
        }

        [Fact]
        public async Task GetAllProductsAsync_WhenProductsExist_ReturnsProducts()
        {
            var product = MakeProduct();

            ProductRepository
                .Setup(repository => repository.FilterPageAsync(1, 10, null, null, null))
                .ReturnsAsync(new PagedResult<Product>
                {
                    Items = [product],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 1
                });

            var result = await ProductServiceUnderTest.GetAllProductsAsync(1, 10, null, null, null);

            Assert.Single(result.Items);
            Assert.Equal(product.Id, result.Items[0].Id);
            Assert.Equal("Sourdough", result.Items[0].Name);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetAllProductsAsync_WhenPaginationIsInvalid_ThrowsValidationError()
        {
            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => ProductServiceUnderTest.GetAllProductsAsync(0, 10, null, null, null));

            ProductRepository.Verify(
                repository => repository.FilterPageAsync(
                    It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ProductCategory?>(),
                    It.IsAny<bool?>(), It.IsAny<string?>()),
                Times.Never);
        }

        [Fact]
        public async Task GetProductByIdAsync_WhenProductDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => ProductServiceUnderTest.GetProductByIdAsync(id));
        }

        [Fact]
        public async Task GetProductByIdAsync_WhenProductExists_ReturnsProduct()
        {
            var id = Guid.NewGuid();
            var product = MakeProduct(id);

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(product);

            var result = await ProductServiceUnderTest.GetProductByIdAsync(id);

            Assert.Equal(id, result.Id);
            Assert.Equal("Sourdough", result.Name);
            Assert.Equal("Bread", result.Category);
            Assert.Equal(10.50m, result.Price);
        }

        [Fact]
        public async Task CreateProductAsync_WhenCategoryIsInvalid_ThrowsValidationError()
        {
            var request = MakeCreateRequest();
            request.Category = "InvalidCategory";

            await Assert.ThrowsAsync<System.ComponentModel.DataAnnotations.ValidationException>(
                () => ProductServiceUnderTest.CreateProductAsync(request));

            ProductRepository.Verify(
                repository => repository.AddAsync(It.IsAny<Product>()),
                Times.Never);
        }

        [Fact]
        public async Task CreateProductAsync_WhenRequestIsValid_CreatesProduct()
        {
            var request = MakeCreateRequest();

            ProductRepository
                .Setup(repository => repository.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) => product);

            var result = await ProductServiceUnderTest.CreateProductAsync(request);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal("Bread", result.Category);
            Assert.Equal(request.Price, result.Price);

            ProductRepository.Verify(
                repository => repository.AddAsync(
                    It.Is<Product>(product => product.Name == request.Name)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenProductDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => ProductServiceUnderTest.UpdateProductAsync(id, MakeUpdateRequest()));
        }

        [Fact]
        public async Task UpdateProductAsync_WhenProductExists_UpdatesProduct()
        {
            var id = Guid.NewGuid();
            var product = MakeProduct(id);
            var request = MakeUpdateRequest();

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(product);

            ProductRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var result = await ProductServiceUnderTest.UpdateProductAsync(id, request);

            Assert.Equal("Updated Sourdough", result.Name);
            Assert.Equal("Pastry", result.Category);
            Assert.Equal(12.00m, result.Price);
            Assert.False(result.IsAvailable);

            ProductRepository.Verify(
                repository => repository.UpdateAsync(
                    It.Is<Product>(updated => updated.Id == id)),
                Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((Product?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => ProductServiceUnderTest.DeleteProductAsync(id));
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductIsNotInAnOrder_DeletesProduct()
        {
            var id = Guid.NewGuid();
            var product = MakeProduct(id);

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(product);

            OrderItemRepository
                .Setup(repository => repository.ExistsByProductIdAsync(id))
                .ReturnsAsync(false);

            ProductRepository
                .Setup(repository => repository.DeleteAsync(product))
                .Returns(Task.CompletedTask);

            await ProductServiceUnderTest.DeleteProductAsync(id);

            ProductRepository.Verify(
                repository => repository.DeleteAsync(
                    It.Is<Product>(deleted => deleted.Id == id)),
                Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductAppearsInAnOrderItem_ThrowsConflict()
        {
            var id = Guid.NewGuid();
            var product = MakeProduct(id);

            ProductRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(product);

            OrderItemRepository
                .Setup(repository => repository.ExistsByProductIdAsync(id))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ConflictException>(
                () => ProductServiceUnderTest.DeleteProductAsync(id));

            ProductRepository.Verify(
                repository => repository.DeleteAsync(It.IsAny<Product>()),
                Times.Never);
        }
    }
}

