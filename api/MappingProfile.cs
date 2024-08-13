using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Item, ItemDto>()
            .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));
        CreateMap<Customer, CustomerDto>()
        .ForMember(dest => dest.FullName, opt =>
        opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        CreateMap<Order, OrderDto>();
        CreateMap<Product, ProductDto>();

    }


}