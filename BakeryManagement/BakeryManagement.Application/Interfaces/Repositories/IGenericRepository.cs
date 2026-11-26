using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Domain.Entities;

namespace BakeryManagement.Application.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task<PagedResult<TEntity>> GetPageAsync(int pageNumber, int pageSize);
        Task<TEntity?> GetAsync(Guid id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
