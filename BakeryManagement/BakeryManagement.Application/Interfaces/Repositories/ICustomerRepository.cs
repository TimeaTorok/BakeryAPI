using BakeryManagement.Domain.Entities;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<PagedResult<Customer>> SearchPageAsync(
            int pageNumber, int pageSize, string? name);
        Task<bool> ExistsByEmailAsync(string email, Guid? excludingCustomerId = null);
    }
}
