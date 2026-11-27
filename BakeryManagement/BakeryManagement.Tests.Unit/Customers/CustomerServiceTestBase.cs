using AutoMapper;
using BakeryManagement.Application.DTOs.Customers;
using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Application.Mapping;
using BakeryManagement.Application.Services;
using BakeryManagement.Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BakeryManagement.Tests.Unit.Customers
{
    public abstract class CustomerServiceTestBase
    {
        protected readonly Mock<ICustomerRepository> CustomerRepository = new();
        protected readonly Mock<IOrderRepository> OrderRepository = new();

        protected readonly CustomerService CustomerServiceUnderTest;

        protected CustomerServiceTestBase()
        {
            var mapperConfiguration = new MapperConfiguration(
                config => config.AddProfile<CustomerMappingProfile>(),
                NullLoggerFactory.Instance);

            var mapper = mapperConfiguration.CreateMapper();

            CustomerServiceUnderTest = new CustomerService(
                CustomerRepository.Object,
                OrderRepository.Object,
                mapper,
                NullLogger<CustomerService>.Instance);
        }

        protected static Customer MakeCustomer(Guid? id = null)
        {
            return new Customer
            {
                Id = id ?? Guid.NewGuid(),
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com",
                PhoneNumber = "555-0100"
            };
        }

        protected static CreateCustomerDto MakeCreateRequest()
        {
            return new CreateCustomerDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com",
                PhoneNumber = "555-0100"
            };
        }

        protected static UpdateCustomerDto MakeUpdateRequest()
        {
            return new UpdateCustomerDto
            {
                FirstName = "Janet",
                LastName = "Smith",
                Email = "janet@example.com",
                PhoneNumber = "555-0101"
            };
        }
    }
}

