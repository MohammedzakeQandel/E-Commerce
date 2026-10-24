using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderModule
{
    public record OrderResult
    { 
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;

        public AddressDto ShippingAddress { get; init; } = default!;

        public ICollection<OrderItemDto> OrderItems { get; init; }
            = new List<OrderItemDto>();

        public string PaymentStatus { get; init; } = string.Empty;

        public string DeliveryMethod { get; init; } = string.Empty!;

        public int? DeliveryMethodId { get; init; }

        // OrderItem1.Q * OrderItem1.Price + OrderItem2.Q * OrderItem2.Price
        public decimal SubTotal { get; init; }

        public decimal Total { get; init; }//SubTotal + deliveryMethod.Price

        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;

        public string PaymentIntentId { get; init; } = string.Empty;
    }
}
