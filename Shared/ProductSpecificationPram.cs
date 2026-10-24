using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationPram
    {
        public int? TypeId {  get; set; }
        public int? BrandId { get; set; }

        public string? Search { get; set; }
        public ProductSortingOptions Sort { get; set; }

        private const int defaultPageSize = 5;
        private const int maxPageSize = 10;
        public int pageIndex { get; set; } = 1;
        private int _pageSize=defaultPageSize;

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? maxPageSize : value; }
        }


    }
}
