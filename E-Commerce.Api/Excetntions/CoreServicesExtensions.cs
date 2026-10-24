using Services;
using Services.Abstraction.Contracts;
using Services.Implementations;
using Shared.Commen;

namespace E_Commerce.Api.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services ,  IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();

            services.AddAutoMapper( cfg => { }, typeof(AssemblyRefrances).Assembly);
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));


            return services;
        }
    }
}