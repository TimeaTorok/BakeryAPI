using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Application.Interfaces.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<PagedResult<Product>> FilterPageAsync(
            int pageNumber, 
            int pageSize, 
            ProductCategory? category, 
            bool? isAvailable,
            string? name);
    }
}
