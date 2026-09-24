using Shop_HW.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Shop_HW.Data
{
    public static class Seeder
    {
        // Extension for WebApplication (minimal hosting). Returns the same app to allow chaining.
        public static WebApplication Seed(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();


            dbContext.Database.Migrate();
            if (!dbContext.Categories.Any())
            {
                var categories = new List<CategoryModel>
                {
                    new CategoryModel
                    {
                        Name = "Electronics",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "iPhone 15 Pro", Description = "Apple iPhone 15 Pro with A17 Bionic chip, 256GB.", Price = 999.99m, Image = "https://example.com/images/iphone15pro.jpg" },
                            new() { Name = "Samsung Galaxy S24", Description = "Samsung Galaxy S24 with powerful camera system.", Price = 899.99m, Image = "https://example.com/images/galaxy_s24.jpg" },
                            new() { Name = "MacBook Air 13", Description = "Apple MacBook Air 13 with M2, 8GB RAM, 256GB SSD.", Price = 1199.99m, Image = "https://example.com/images/macbookair13.jpg" },
                            new() { Name = "Sony WH-1000XM5", Description = "Noise cancelling wireless headphones.", Price = 349.99m, Image = "https://example.com/images/sony_wh1000xm5.jpg" },
                            new() { Name = "Apple iPad Air", Description = "10.9-inch iPad Air with M1 chip.", Price = 599.99m, Image = "https://example.com/images/ipad_air.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "Literature",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "The Shining (Stephen King)", Description = "A classic horror novel by Stephen King.", Price = 14.99m, Image = "https://example.com/images/the_shining.jpg" },
                            new() { Name = "It (Stephen King)", Description = "Horror novel about a shape-shifting entity.", Price = 18.99m, Image = "https://example.com/images/it_stephen_king.jpg" },
                            new() { Name = "1984 (George Orwell)", Description = "Dystopian novel by George Orwell.", Price = 12.99m, Image = "https://example.com/images/1984_orwell.jpg" },
                            new() { Name = "The Lord of the Rings (J.R.R. Tolkien)", Description = "Epic fantasy trilogy.", Price = 29.99m, Image = "https://example.com/images/lotr.jpg" },
                            new() { Name = "Murder on the Orient Express (Agatha Christie)", Description = "Classic mystery novel.", Price = 9.99m, Image = "https://example.com/images/orient_express.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "Furniture",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "Modern Sofa", Description = "3-seater fabric sofa, grey.", Price = 499.99m, Image = "https://example.com/images/sofa_modern.jpg" },
                            new() { Name = "Dining Table", Description = "Solid wood dining table for 6.", Price = 699.99m, Image = "https://example.com/images/dining_table.jpg" },
                            new() { Name = "Office Chair Ergonomic", Description = "Adjustable lumbar support, mesh back.", Price = 199.99m, Image = "https://example.com/images/office_chair.jpg" },
                            new() { Name = "Queen Bed Frame", Description = "Upholstered bed frame with headboard.", Price = 399.99m, Image = "https://example.com/images/bed_frame.jpg" },
                            new() { Name = "Bookshelf 5-tier", Description = "Industrial-style bookshelf.", Price = 149.99m, Image = "https://example.com/images/bookshelf.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "Clothes & Accessories",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "Classic T-Shirt", Description = "100% cotton, unisex.", Price = 19.99m, Image = "https://example.com/images/tshirt_classic.jpg" },
                            new() { Name = "Blue Jeans", Description = "Slim fit denim jeans.", Price = 49.99m, Image = "https://example.com/images/jeans_blue.jpg" },
                            new() { Name = "Running Sneakers", Description = "Lightweight running shoes.", Price = 89.99m, Image = "https://example.com/images/sneakers.jpg" },
                            new() { Name = "Classic Watch", Description = "Analog watch with leather strap.", Price = 149.99m, Image = "https://example.com/images/watch_classic.jpg" },
                            new() { Name = "Leather Backpack", Description = "Everyday leather backpack.", Price = 129.99m, Image = "https://example.com/images/backpack_leather.jpg" }
                        }
                    }
                };

                dbContext.Categories.AddRange(categories);
                dbContext.SaveChanges();
            }

            return app;
        }
    }
}
