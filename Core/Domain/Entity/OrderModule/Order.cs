using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress = Domain.Entity.OrderModule.Adress;

namespace Domain.Entity.OrderModule
{

    public class Order : BaseEntity<Guid>
    {
        public Order(string userEmail, ShippingAddress shippingAddress, ICollection<OrderItem> orderItems,
            DeliveryMethod deliveryMethod, decimal subTotal , string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public Order()
        {
            
        }
        public string UserEmail { get; set; } = string.Empty;

        public ShippingAddress ShippingAddress { get; set; } = default!;

        public ICollection<OrderItem> OrderItems { get; set; }
            = new List<OrderItem>();

        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public DeliveryMethod DeliveryMethod { get; set; } = default!;

        public int? DeliveryMethodId { get; set; }

        // OrderItem1.Q * OrderItem1.Price + OrderItem2.Q * OrderItem2.Price
        public decimal SubTotal { get; set; }

        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;

        public string PaymentIntentId { get; set; } = string.Empty;
    }

}
