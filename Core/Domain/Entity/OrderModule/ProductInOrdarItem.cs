using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.OrderModule
{
    public class ProductInOrdarItem
    {
        public ProductInOrdarItem(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }
        public ProductInOrdarItem()
        {
            
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }=string.Empty;
        public string PictureUrl {  get; set; }= string.Empty;
    }
}
