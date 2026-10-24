using Domain.Entity.BasketModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contrects
{
    public interface IBasketRepo
    {
        //Get basket by id
        Task<BasketCustomer?> GetBasketAsync(string id);
        //Create Or Ubdate basket
        Task<BasketCustomer?> CreateOrUbdate(BasketCustomer basket , TimeSpan? timeTolive = null);
        //Delete basket
        Task<bool> DeletBasketAsunc(string id);
            
    }
}
