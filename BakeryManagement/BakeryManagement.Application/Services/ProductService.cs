using System.ComponentModel.DataAnnotations;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.DTOs.Products;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Interfaces.Services;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BakeryManagement.Application.Services
{
    public class ProductService(
        IProductRepository productRepository,
        IOrderItemRepository orderItemRepository,
        IMapper mapper,
        ILogger<ProductService> logger) : IProductService
    {
        private const int MaximumNameLength = 200;
        private const int MaximumDescriptionLength = 1000;
        private const decimal MaximumPrice = 99_999_999.99m;

        public async Task<PagedResult<ProductDto>> GetAllProductsAsync(
            int pageNumber,
            int pageSize,
            string? category,
            bool? isAvailable)
        {
            PaginationValidator.Validate(pageNumber, pageSize);

            ProductCategory? categoryFilter = string.IsNullOrWhiteSpace(category)
                ? null
                : ParseCategory(category);

            var page = await productRepository.FilterPageAsync(
                pageNumber, pageSize, categoryFilter, isAvailable);

            logger.LogInformation(
                "Retrieved products page {PageNumber} with page size {PageSize}, category {Category}, availability {IsAvailable}; returned {ResultCount} of {TotalCount} products",
                page.PageNumber,
                page.PageSize,
                category,
                isAvailable,
                page.Items.Count,
                page.TotalCount);

            return new PagedResult<ProductDto>
            {
                Items = mapper.Map<List<ProductDto>>(page.Items),
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                TotalCount = page.TotalCount
            };
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
             var product = await productRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Product '{id}' was not found.");

            logger.LogInformation("Retrieved product {ProductId}", id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto request)
        {
            ValidateProduct(
                request.Name,
                request.Description,
                request.Category,
                request.Price);

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            logger.LogInformation("Created product {ProductId}", product.Id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto request)
        {
            var product = await productRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Product '{id}' was not found.");

            ValidateProduct(
                request.Name,
                request.Description,
                request.Category,
                request.Price);

            mapper.Map(request, product);

            await productRepository.UpdateAsync(product);
            logger.LogInformation("Updated product {ProductId}", product.Id);
            return mapper.Map<ProductDto>(product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await productRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Product '{id}' was not found.");

            if (await orderItemRepository.ExistsByProductIdAsync(id))
                throw new ConflictException(
                    "Cannot delete a product that appears in an order.");

            await productRepository.DeleteAsync(product);
            logger.LogInformation("Deleted product {ProductId}", id);
        }

        private static ProductCategory ValidateProduct(
            string name,
            string? description,
            string category,
            decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Product name is required.");

            if (name.Trim().Length > MaximumNameLength)
                throw new ValidationException(
                    $"Product name cannot exceed {MaximumNameLength} characters.");

            if (description?.Length > MaximumDescriptionLength)
                throw new ValidationException(
                    $"Product description cannot exceed {MaximumDescriptionLength} characters.");

            if (price < 0 || price > MaximumPrice || decimal.Round(price, 2) != price)
                throw new ValidationException(
                    $"Product price must be between 0 and {MaximumPrice} with no more than two decimal places.");

            return ParseCategory(category);
        }

        private static ProductCategory ParseCategory(string? category)
        {
            var enumName = Enum.GetNames<ProductCategory>()
                .FirstOrDefault(name => string.Equals(
                    name,
                    category?.Trim(),
                    StringComparison.OrdinalIgnoreCase));

            if (enumName is null)
                throw new ValidationException(
                    "Product category must be Bread, Pastry, Cake, Cookie, Muffin, or Other.");

            return Enum.Parse<ProductCategory>(enumName);
        }

    }
}
