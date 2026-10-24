using AutoMapper;
using Domain.Contracts;
using Domain.Contrects;
using Domain.Entity.BasketModules;
using Domain.Entity.OrderModule;
using Domain.Entity.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class OrderService (IMapper _mapper , IBasketRepo _basketRepo ,IUnitOfWork _unitOfWork): IorderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // 1. Map AddressDto to Address
            var address = _mapper.Map<Adress>(orderRequest.ShipToAddress);

            // 2. Get Basket ==> BasketItems ==> OrderItems
            var basket = await _basketRepo.GetBasketAsync(orderRequest.BasketId)?? throw new BasketNotFoundException(orderRequest.BasketId);

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(product, item));
            }
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            // 3. Get Delivery Method from DB
            var deliveryMethod = await _unitOfWork .GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException( orderRequest.DeliveryMethodId);
            var orderExisit = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpesification(basket.PaymentIntentId));
            if (orderExisit != null)
            {
                orderRepo.Delete(orderExisit);
            }

            // 4. Calculate SubTotal
            var subTotal = orderItems.Sum(o => o.Price * o.Quantity);

            // 5. Create Order, Add to DB, Save Changes
            var orderToCreate = new Order(
                userEmail,
                address,
                orderItems,
                deliveryMethod,
                subTotal,
                basket.PaymentIntentId
                );

            await orderRepo.AddAsync(orderToCreate);

            await _unitOfWork.SaveChangesAsync();

            // 6. Map Order to OrderResult
            return _mapper.Map<OrderResult>(orderToCreate);
        }

        private OrderItem CreateOrderItem(Product product, BasketItem item)
        {
            var productInOrderItem = new ProductInOrdarItem(
                product.Id,
                product.Name,
                product.PictureUrl);

            return new OrderItem(
                productInOrderItem,
                product.Price,
                item.Quantity);
        }
  

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods =await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork .GetRepository<Order, Guid>() .GetByIdAsync(new OrderWithIncludesSpasifications(id))
        ?? throw new OrderNotFoundException(id);

            return _mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludesSpasifications(userEmail));

            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
    }
}
