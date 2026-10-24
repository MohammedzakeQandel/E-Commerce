using Domain.Entity.ProductModule;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProduvtWithBrandAndTypeSpecifications:BaseSpecification<Product,int>
    {
        public ProduvtWithBrandAndTypeSpecifications(ProductSpecificationPram pram) 
            : base(p=>(!pram.TypeId.HasValue ||p.TypeId==pram.TypeId) &&
                  (!pram.BrandId.HasValue|| p.BrandId== pram.BrandId)&&
                   (string.IsNullOrEmpty(pram.Search)||p.Name.ToLower().Contains(pram.Search.ToLower()))) 
        {
            AddInclude(p => p.ProductType);
            AddInclude( p => p.ProductBrand);
            switch (pram.Sort) {

                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesending(p => p.Name); 
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesending(p => p.Price);
                    break;

                 default:
                    break;
            }
            ApplyPagination(pram.pageSize, pram.pageIndex);
        }
        public ProduvtWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
