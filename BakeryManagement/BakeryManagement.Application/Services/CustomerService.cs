using BakeryManagement.Application.DTOs.Customers;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Interfaces.Services;
using BakeryManagement.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BakeryManagement.Application.Services
{
    public class CustomerService(
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        IMapper mapper,
        ILogger<CustomerService> logger) : ICustomerService
    {
        public async Task<PagedResult<CustomerDto>> GetAllCustomersAsync(
            int pageNumber,
            int pageSize,
            string? name)
        {
            PaginationValidator.Validate(pageNumber, pageSize);
            var page = await customerRepository.SearchPageAsync(
                pageNumber, pageSize, name?.Trim());
            logger.LogInformation(
                "Retrieved customers page {PageNumber} with page size {PageSize}, name {Name}; returned {ResultCount} of {TotalCount} customers",
                page.PageNumber,
                page.PageSize,
                name,
                page.Items.Count,
                page.TotalCount);

            return new PagedResult<CustomerDto>
            {
                Items = mapper.Map<List<CustomerDto>>(page.Items),
                PageNumber = page.PageNumber,
                PageSize = page.PageSize,
                TotalCount = page.TotalCount
            };
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id)
        {
            var customer = await customerRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Customer '{id}' was not found.");

            logger.LogInformation("Retrieved customer {CustomerId}", id);
            return mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto request)
        {
            var email = request.Email.Trim();
            if (await customerRepository.ExistsByEmailAsync(email))
                throw new ConflictException("A customer with this email already exists.");

            var customer = mapper.Map<Customer>(request);
            customer.Email = email;
            await customerRepository.AddAsync(customer);
            logger.LogInformation("Created customer {CustomerId}", customer.Id);
            return mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(Guid id, UpdateCustomerDto request)
        {
            var customer = await customerRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Customer '{id}' was not found.");

            var email = request.Email.Trim();
            if (await customerRepository.ExistsByEmailAsync(email, id))
                throw new ConflictException("A customer with this email already exists.");

            mapper.Map(request, customer);
            customer.Email = email;
            await customerRepository.UpdateAsync(customer);
            logger.LogInformation("Updated customer {CustomerId}", customer.Id);
            return mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await customerRepository.GetAsync(id)
                ?? throw new EntityNotFoundException($"Customer '{id}' was not found.");

            if (await orderRepository.ExistsByCustomerIdAsync(id))
                throw new ConflictException(
                    "Cannot delete a customer that has orders.");

            await customerRepository.DeleteAsync(customer);
            logger.LogInformation("Deleted customer {CustomerId}", id);
        }
    }
}
