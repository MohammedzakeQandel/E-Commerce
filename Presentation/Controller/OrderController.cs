using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    
    public class OrderController(IServiceManager _serviceManager):ApiController
    {
        // Create Order
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(
            OrderRequest orderRequest)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email)!;

            var order = await _serviceManager.OrderService
                .CreateOrderAsync(orderRequest, userEmail);

            return Ok(order);
        }

        // Get Order By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid id)
        {
            var order = await _serviceManager.OrderService
                .GetOrderByIdAsync(id);

            return Ok(order);
        }

        // Get All Orders By Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrdersByEmail()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email)!;

            var orders = await _serviceManager.OrderService
                .GetOrdersByEmailAsync(userEmail);

            return Ok(orders);
        }

        // Get Delivery Methods
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _serviceManager.OrderService
                .GetDeliveryMethodsAsync();

            return Ok(deliveryMethods);
        }

    }
}
