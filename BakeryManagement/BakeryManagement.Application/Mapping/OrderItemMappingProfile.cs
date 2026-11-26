using AutoMapper;
using BakeryManagement.Application.DTOs.OrderItems;
using BakeryManagement.Domain.Entities;

namespace BakeryManagement.Application.Mapping
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItem, OrderItemSummaryDto>();
        }
    }
}
