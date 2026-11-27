using AutoMapper;
using BakeryManagement.Application.DTOs.Products;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Mapping;
using BakeryManagement.Application.Services;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BakeryManagement.Tests.Unit.Products
{
    public abstract class ProductServiceTestBase
    {
        protected readonly Mock<IProductRepository> ProductRepository = new();
        protected readonly Mock<IOrderItemRepository> OrderItemRepository = new();

        protected readonly ProductService ProductServiceUnderTest;

        protected ProductServiceTestBase()
        {
            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<ProductMappingProfile>(),
                NullLoggerFactory.Instance);

            var mapper = mapperConfiguration.CreateMapper();

            ProductServiceUnderTest = new ProductService(
                ProductRepository.Object,
                OrderItemRepository.Object,
                mapper,
                NullLogger<ProductService>.Instance);
        }

        protected static Product MakeProduct(Guid? id = null)
        {
            return new Product
            {
                Id = id ?? Guid.NewGuid(),
                Name = "Sourdough",
                Description = "Fresh bread",
                Category = ProductCategory.Bread,
                Price = 10.50m,
                IsAvailable = true
            };
        }

        protected static CreateProductDto MakeCreateRequest()
        {
            return new CreateProductDto
            {
                Name = "Sourdough",
                Description = "Fresh bread",
                Category = "Bread",
                Price = 10.50m,
                IsAvailable = true
            };
        }

        protected static UpdateProductDto MakeUpdateRequest()
        {
            return new UpdateProductDto
            {
                Name = "Updated Sourdough",
                Description = "Updated description",
                Category = "Pastry",
                Price = 12.00m,
                IsAvailable = false
            };
        }
    }
}

