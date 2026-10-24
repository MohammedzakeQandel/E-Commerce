
using E_Commerce.Api.Extensions;

namespace E_Commerce.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {


            #region Dependency Injection
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddWebApiServices(builder.Configuration);

            builder.Services.AddCoreServices(builder.Configuration);

            builder.Services.AddInfrastructureServices(
                builder.Configuration);

            #endregion


            #region HTTP Request Pipeline

            var app = builder.Build();

            await app.SeedDatabaseAsync();

            app.UseExceptionHandlingMiddlewares();

            app.UseSwaggerMiddlewares();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
            #endregion


        }
    }
}
