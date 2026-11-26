using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(BakeryDbContext context) : IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly BakeryDbContext _context = context;

        public async Task<PagedResult<TEntity>> GetPageAsync(int pageNumber, int pageSize)
        {
            var query = _context.Set<TEntity>().AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(entity => entity.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public Task<TEntity?> GetAsync(Guid id) =>
            _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
