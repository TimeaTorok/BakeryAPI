using AutoMapper;
using BakeryManagement.Application.DTOs.Products;
using BakeryManagement.Domain.Entities;

namespace BakeryManagement.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
