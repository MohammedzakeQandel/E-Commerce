using Domain.Contracts;
using Domain.Entity.IdentityModule;
using Domain.Entity.OrderModule;
using Domain.Entity.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System.Text.Json;

namespace Presistence
{
   
        public class DataSeeding(StoreDbContext _dbContext ,
            RoleManager<IdentityRole> _roleManager,
            UserManager<User> _userManager  ) : IDataSeeding
        {
            public async Task SeedDataAsync()
            {
                try
                {
                    if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                    {
                        await _dbContext.Database.MigrateAsync();
                    }

                    // Product Types
                    if (!await _dbContext.ProductTypes.AnyAsync())
                    {
                        var productTypesData = await File.ReadAllTextAsync(
                            "..\\Infrastructure\\Prrsistence\\Data\\DataSeed\\Types.json");

                        var productTypes =
                            JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

                        if (productTypes is not null && productTypes.Any())
                        {
                            await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                        }
                    }

                    // Product Brands
                    if (!await _dbContext.ProductBrands.AnyAsync())
                    {
                        var productBrandData = await File.ReadAllTextAsync(
                            "..\\Infrastructure\\Prrsistence\\Data\\DataSeed\\brands.json");

                        var productBrands =
                            JsonSerializer.Deserialize<List<ProductBrand>>(productBrandData);

                        if (productBrands is not null && productBrands.Any())
                        {
                            await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                        }
                    }

                    // Products
                    if (!await _dbContext.Products.AnyAsync())
                    {
                        var productData = await File.ReadAllTextAsync(
                            "..\\Infrastructure\\Prrsistence\\Data\\DataSeed\\products.json");

                        var products =
                            JsonSerializer.Deserialize<List<Product>>(productData);

                        if (products is not null && products.Any())
                        {
                            await _dbContext.Products.AddRangeAsync(products);
                        }
                    }
                if (!await _dbContext.DeliveryMethods.AnyAsync())
                {
                    var DeliveryMethodsData = await File.ReadAllTextAsync(
                        "..\\Infrastructure\\Prrsistence\\Data\\DataSeed\\delivery.json");

                    var Delivary =
                        JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);

                    if (Delivary is not null && Delivary.Any())
                    {
                        await _dbContext.DeliveryMethods.AddRangeAsync(Delivary);
                    }
                }
                await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                // 1) Seed roles [Admin, SuperAdmin]
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                // 2) Seed users [AdminUser, SuperAdminUser]
                if (!_userManager.Users.Any())
                {
                    var adminUser = new User
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "01098764532"
                    };

                    var superAdminUser = new User
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01098764530"
                    };

                    await _userManager.CreateAsync(adminUser, "P@sswOrd123");
                    await _userManager.CreateAsync(superAdminUser, "Pa$$word123");

                    // 3) Assign roles to users
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    }
