using AutoMapper;
using Domain.Entity.ProductModule;
using Shared.Dtos.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class ProductFile: Profile
    {
        public ProductFile() 
        {
            CreateMap<Product, ProductResultDto>()
               .ForMember(des => des.TypeName, option => option.MapFrom(src => src.ProductType.Name))
               .ForMember(des => des.BrandName, option => option.MapFrom(src => src.ProductBrand.Name))
               .ForMember(des => des.PictureUrl,option => option.MapFrom<PictureUrIResolver>());

            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();    

        }
    }
}
