using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.ProductModule
{
    public record ProductResultDto
    {
        public int Id { get; set; }//not to displey the front needed
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string BrandName {  get; set; } = string.Empty;
        public string TypeName {  get; set; } = string.Empty;


    }
}
