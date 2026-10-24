using AutoMapper;
using Domain.Entity.IdentityModule;
using Domain.Entity.OrderModule;
using Microsoft.Extensions.Options;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Domain.Entity.OrderModule.Adress, AddressDto>() .ReverseMap();
            CreateMap<Domain.Entity.IdentityModule.Adress, AddressDto>() .ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>().ForMember(dest => dest.Cost, optons => optons.MapFrom(src=>src.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId,
                    options => options.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName,
                    options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl,
                    options => options.MapFrom(src => src.Product.PictureUrl));

            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.PaymentStatus,
                    options => options.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod,
                    options => options.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total,
                    options => options.MapFrom(src => src.SubTotal + src.DeliveryMethod.Price));
        }
    }
}
