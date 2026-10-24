using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.ProductModule
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }
        //1-m ProductType
        public ProductType ProductType { get; set; }//nv
        public int TypeId { get; set; }
        //1-m ProductBrand
        public ProductBrand ProductBrand { get; set; }//nv
        public int BrandId { get; set; }
    }
}
