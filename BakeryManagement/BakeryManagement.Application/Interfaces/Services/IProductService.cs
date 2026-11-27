using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.DTOs.Products;

namespace BakeryManagement.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<PagedResult<ProductDto>> GetAllProductsAsync(
            int pageNumber, 
            int pageSize, 
            string? category, 
            bool? isAvailable,
            string? name);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductDto request);
        Task<ProductDto> UpdateProductAsync(Guid id, UpdateProductDto request);
        Task DeleteProductAsync(Guid id);
    }
}
