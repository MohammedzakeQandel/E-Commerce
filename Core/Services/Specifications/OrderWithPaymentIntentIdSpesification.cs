using Domain.Entity.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentIdSpesification : BaseSpecification<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpesification(string paymentIntentId):base(o=>o.PaymentIntentId==paymentIntentId) { }
    }
}
