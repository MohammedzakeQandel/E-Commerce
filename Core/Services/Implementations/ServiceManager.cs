using AutoMapper;
using Domain.Contracts;
using Domain.Contrects;
using Domain.Entity.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction.Contracts;
using Shared.Commen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepo _basketRepo
        ,UserManager<User>_userManager, IOptions<JwtOptions> _options , IConfiguration _configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _BasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepo, _mapper));
        private readonly Lazy<IAutenticationService> _authSevice = new Lazy<IAutenticationService>(() => new AutenticationService(_userManager,_options,_mapper));
        private readonly Lazy<IorderService> _orderService = new Lazy<IorderService>(() => new OrderService(_mapper, _basketRepo, _unitOfWork));
        private readonly Lazy<IPaymentService> _paymentService = new Lazy<IPaymentService>(() => new PaymentService(_configuration,_basketRepo,_unitOfWork,_mapper));

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _BasketService.Value;

        public IAutenticationService AutenticationService => _authSevice.Value;

        public IorderService OrderService => _orderService.Value;

        public IPaymentService PaymentService => _paymentService.Value;
    }
}
