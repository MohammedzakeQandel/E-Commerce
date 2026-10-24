using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class PaymentsController (IServiceManager _serviceManager):ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent( string basketId)
        {
            var basket = await _serviceManager.PaymentService .CreateOrUpdatePaymentIntentAsync(basketId);

            return Ok(basket);
        }
    }
}
