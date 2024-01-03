using AutoMapper;
using Project.Services.Order.BLL.DTOs;
using Project.Services.Order.Domain.OrderAggregate;


namespace Project.Services.Order.BLL.Mapping
{
    internal class CustomMapping: Profile
    {
        public CustomMapping()
        {
            CreateMap<Domain.OrderAggregate.Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
