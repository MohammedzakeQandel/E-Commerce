using Domain.Contrects;
using Domain.Entity.BasketModules;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    public class BasketRepo (IConnectionMultiplexer _connection ): IBasketRepo
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<BasketCustomer?> CreateOrUbdate(BasketCustomer basket, TimeSpan? timeTolive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var res = await _database.StringSetAsync(basket.Id, jsonBasket,timeTolive ?? TimeSpan.FromDays(30));
            return res ? await GetBasketAsync(basket.Id) : null;
        }

        public async Task<bool> DeletBasketAsunc(string id)
        => await _database.KeyDeleteAsync(id);  

        public async Task<BasketCustomer?> GetBasketAsync(string id)
        {
            var res =await _database.StringGetAsync(id);
            if(res.IsNullOrEmpty)return null;
            return JsonSerializer.Deserialize<BasketCustomer?>(res!);
        }
    }
}
