using Domain.Entity.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductCountSpecification:BaseSpecification<Product,int>
    {
        public ProductCountSpecification(ProductSpecificationPram pram):base(p => (!pram.TypeId.HasValue || p.TypeId == pram.TypeId) &&
                  (!pram.BrandId.HasValue || p.BrandId == pram.BrandId) &&
                   (string.IsNullOrEmpty(pram.Search) || p.Name.ToLower().Contains(pram.Search.ToLower())))
        {

        }
    }
}
