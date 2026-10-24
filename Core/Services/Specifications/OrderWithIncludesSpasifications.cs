using Domain.Entity.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludesSpasifications : BaseSpecification<Order,Guid>
    {
        // Get Order By Id
        // Criteria: Id == id
        // Includes: DeliveryMethod, OrderItems
        public OrderWithIncludesSpasifications(Guid id)
            : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }

        // Get All Orders By Email
        // Criteria: UserEmail == userEmail
        // Includes: DeliveryMethod, OrderItems
        // Order By: OrderDate descending
        public OrderWithIncludesSpasifications(string userEmail)
            : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);

            AddOrderByDesending(o => o.OrderDate);
        }
    }
}
