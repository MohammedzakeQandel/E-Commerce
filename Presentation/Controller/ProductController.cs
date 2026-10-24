using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos.ProductModule;
using Shared.Enums;
using Shared.ErroreModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
  
    public class ProductsController(IServiceManager _serviceManager) : ApiController
    {
        //Get All Product
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationPram pram)
        {
            var products = await _serviceManager.ProductService.GetAllProductsAsync(pram);

            return Ok(products);
        }

        // Get all product brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await _serviceManager.ProductService.GetAllBrandsAsync();

            return Ok(brands);
        }

        // Get all product types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var types = await _serviceManager.ProductService.GetAllTypesAsync();

            return Ok(types);
        }

        // Get product by Id
        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);

            return Ok(product);
        }
    }
}
