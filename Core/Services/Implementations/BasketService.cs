using AutoMapper;
using Domain.Contrects;
using Domain.Entity.BasketModules;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class BasketService(IBasketRepo _basketRepo, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> CreateorUpdateAsync(BasketDto basketDto)
        {
            var basket = _mapper.Map<BasketCustomer>(basketDto);

            var createOrUpdateCustomerBasket = await _basketRepo.CreateOrUbdate(basket);
            return createOrUpdateCustomerBasket is null ? throw new Exception("can not creat or update basket") :
                _mapper.Map<BasketDto>(createOrUpdateCustomerBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        => await _basketRepo.DeletBasketAsunc(id);

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket =await _basketRepo.GetBasketAsync(id);
            return basket is null?throw new BasketNotFoundException(id): _mapper.Map<BasketDto>(basket);
        }
    }
}
