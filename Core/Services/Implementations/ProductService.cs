using AutoMapper;
using Domain.Contracts;
using Domain.Entity.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    internal class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(); 
            var brandMapp = _mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return brandMapp;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationPram pram)
        {
            var productRebo =  _unitOfWork.GetRepository<Product, int>();
            var spc = new ProduvtWithBrandAndTypeSpecifications( pram);
            var products = await productRebo.GetAllAsync(spc);
            var productResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            var pageSize = productResult.Count();
            var countSpicifacation = new ProductCountSpecification(pram);
            var totalCount = await productRebo.CountAsync(countSpicifacation);
            return new PaginatedResult<ProductResultDto>(pram.pageIndex, pageSize, totalCount, productResult);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypeMapp = _mapper.Map<IEnumerable<TypeResultDto>>(Types);
            return TypeMapp;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var spc = new ProduvtWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product,int>().GetByIdAsync(spc);
            //var productMapp= _mapper.Map<ProductResultDto>(product);
            //return productMapp;
            return product is null?throw new ProductNotFoundException(id): _mapper.Map<ProductResultDto>(product);
        }
    }
}
