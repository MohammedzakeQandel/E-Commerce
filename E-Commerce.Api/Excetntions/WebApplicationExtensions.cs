
using Domain.Contracts;
using E_Commerce.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Data;

namespace E_Commerce.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await dataSeeding.SeedDataAsync();
            await dataSeeding.SeedIdentityDataAsync();

            return app;
        }

        public static WebApplication UseExceptionHandlingMiddlewares( this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddlewar>();

            return app;
        }

        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
