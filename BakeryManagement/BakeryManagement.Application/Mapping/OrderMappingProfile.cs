using AutoMapper;
using BakeryManagement.Application.DTOs.Orders;
using BakeryManagement.Domain.Entities;

namespace BakeryManagement.Application.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderWithItemsDto>();
        }
    }
}
