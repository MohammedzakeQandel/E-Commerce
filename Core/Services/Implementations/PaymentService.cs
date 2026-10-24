using AutoMapper;
using Domain.Contracts;
using Domain.Contrects;
using Domain.Entity.BasketModules;
using Product= Domain.Entity.ProductModule.Product;
using Domain.Entity.ProductModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.OrderModule;

namespace Services.Implementations
{
    public class PaymentService(IConfiguration _configuration, IBasketRepo _basketRepo
        , IUnitOfWork _unitOfWork,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];

            var basket = await GetBasketAsync(basketId);

            await ValidateBasketAsync(basket);

            var amount = CalculateTotalAsync(basket);

            await CreationOrUpdatePaymentIntentAsync(basket, amount);

            await _basketRepo.CreateOrUbdate(basket);

            return _mapper.Map<BasketDto>(basket);
        }
        private async Task ValidateBasketAsync(BasketCustomer basket)
        {
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork
                    .GetRepository<Product,int>()
                    .GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
            }

            if (!basket.DeliveryMethodId.HasValue)
                throw new Exception("No delivery method selected");

            var deliveryMethod = await _unitOfWork
                .GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(
                    basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Price;
        }
        private long CalculateTotalAsync(BasketCustomer basket)
        {
            var amount = (long)(
                (basket.Items.Sum(item => item.Quantity * item.Price)
                + basket.ShippingPrice.GetValueOrDefault()) * 100
            );

            return amount;
        }
        private async Task<BasketCustomer> GetBasketAsync(string basketId)
        {
            return await _basketRepo.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);
        }

        private async Task CreationOrUpdatePaymentIntentAsync(
            BasketCustomer basket,
            long amount)
        {
            var stripeService = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create Payment Intent
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await stripeService.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update Payment Intent
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };

                await stripeService.UpdateAsync(
                    basket.PaymentIntentId,
                    options);
            }
        }
    }
}