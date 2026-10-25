using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop_HW.Models;
using System.Text.Json;
namespace Shop_HW.Data
{
    public static class Seeder
    {
        // Extension for WebApplication (minimal hosting). Returns the same app to allow chaining.
        public async static Task Seed(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();


            dbContext.Database.Migrate();


            if (!roleManager.Roles.Any())
            {
                var adminRole = new IdentityRole { Name = "admin" };
                var userRole = new IdentityRole { Name = "user" };
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);

                var admin = new ApplicationUser
                {
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Smith"
                };

                var user = new ApplicationUser
                {
                    Email = "user@mail.com",
                    EmailConfirmed = true,
                    UserName = "user",
                    FirstName = "Mike",
                    LastName = "Thomson"
                };
                await userManager.CreateAsync(admin, "qwerty");
                await userManager.CreateAsync(user, "qwerty");

                await userManager.AddToRoleAsync(admin, "admin");
                await userManager.AddToRoleAsync(user, "user");
            }

            if (!dbContext.Categories.Any())
            {
                string filePath = Path.Combine(environment.WebRootPath, "seed_data", "products_categories.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var categories = JsonSerializer.Deserialize<List<CategoryModel>>(json);
                    if (categories != null)
                    {
                        foreach (var category in categories)
                        {
                            foreach (var product in category.Products)
                            {
                                product.Rating = new Random().Next(1, 6);
                            }
                        }
                    }
                    dbContext.Categories.AddRange(categories);
                    dbContext.SaveChanges();
                }       
            }

           
        }
    }
}
