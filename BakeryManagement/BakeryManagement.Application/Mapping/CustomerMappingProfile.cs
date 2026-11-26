using AutoMapper;
using BakeryManagement.Application.DTOs.Customers;
using BakeryManagement.Domain.Entities;

namespace BakeryManagement.Application.Mapping
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
        }
    }
}
