using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        // GET: BaseUrl/api/Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.GetBasketAsync(id));

        // POST: BaseUrl/api/Basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
            => Ok(await _serviceManager.BasketService.CreateorUpdateAsync(basket));

        // DELETE: BaseUrl/api/Basket/Basket01
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }

    }
}
