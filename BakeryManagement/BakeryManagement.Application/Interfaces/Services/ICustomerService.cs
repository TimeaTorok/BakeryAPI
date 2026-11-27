using BakeryManagement.Application.DTOs.Customers;
using BakeryManagement.Application.DTOs.Pagination;

namespace BakeryManagement.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<PagedResult<CustomerDto>> GetAllCustomersAsync(
            int pageNumber, int pageSize, string? name);
        Task<CustomerDto> GetCustomerByIdAsync(Guid id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto request);
        Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto request);
        Task DeleteCustomerAsync(Guid id);
    }
}
