using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction.Contracts
{
    public interface IProductService
    {
        // Get all products
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationPram pram);

        // Get all product brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        // Get all product types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

        // Get a single product by its Id
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
