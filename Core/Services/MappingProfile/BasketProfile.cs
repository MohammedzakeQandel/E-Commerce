using AutoMapper;
using Domain.Entity.BasketModules;
using Shared.Dtos.BasketModule;

namespace Services.MappingProfile
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCustomer, BasketDto>()
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src => src.Items))
                .ReverseMap()
                .ForMember(
                    dest => dest.Items,
                    opt => opt.MapFrom(src => src.Items));

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}