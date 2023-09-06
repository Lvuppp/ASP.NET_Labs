using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.Domain.Entities;

namespace Web_153504_Bagrovets.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            List<Category> categories = new List<Category>(){
                new Category {Name = "Product", NormalizedName = "product" },
                new Category {Name = "Technic", NormalizedName = "technic" } };

            List<Product> products = new List<Product>()
            {
                new Product{Name = "Product1", Description = "Description1", Price = 1,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image ="Item.png"},
                new Product{Name = "Product2", Description = "Description2", Price = 2,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image ="Item.png"},
                new Product{Name = "Technic1", Description = "Description3", Price = 3,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image ="Item.png"},
                new Product{ Name = "Technic2", Description = "Description4", Price = 4,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image ="Item.png"},
                new Product{Name = "Product3", Description = "Description5", Price = 5,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image ="Item.png"},
                new Product{ Name = "Product4", Description = "Description6", Price = 6,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image = "Item.png"},
                new Product{Name = "Technic3", Description = "Description7", Price = 7,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image = "Item.png"},
                new Product{Name = "Technic4", Description = "Description8", Price = 8,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image ="Item.png"},
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            foreach(var product in products)
            {
                context.Products.Add(product);
            }
            await context.SaveChangesAsync();

            //await context.Database.MigrateAsync();
        }
    }
}
