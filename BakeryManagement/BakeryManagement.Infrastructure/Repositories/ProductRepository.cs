using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Domain.Enums;
using BakeryManagement.Application.DTOs.Pagination;
using Microsoft.EntityFrameworkCore;
using BakeryManagement.Infrastructure.Persistence;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class ProductRepository(BakeryDbContext context)
        : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<PagedResult<Product>> FilterPageAsync(
            int pageNumber, 
            int pageSize, 
            ProductCategory? category, 
            bool? isAvailable,
            string? name)
        {
            var query = _context.Products.AsNoTracking();
            if (category.HasValue) 
                query = query.Where(product => product.Category == category.Value);
            if (isAvailable.HasValue) 
                query = query.Where(product => product.IsAvailable == isAvailable.Value);
            if (!string.IsNullOrWhiteSpace(name))
            {
                var searchTerm = name.ToLower();
                query = query.Where(product => product.Name.ToLower().Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(product => product.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedResult<Product> 
            { 
                Items = items, 
                PageNumber = pageNumber, 
                PageSize = pageSize, 
                TotalCount = totalCount };
        }
    }
}
