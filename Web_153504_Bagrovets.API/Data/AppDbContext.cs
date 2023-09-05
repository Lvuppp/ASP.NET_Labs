using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.Domain.Entities;

namespace Web_153504_Bagrovets.API.Data
{
    public class AppDbContext : DbContext
    {
        private DbSet<Category> Categories { get; set; } = null!;
        private DbSet<Product> Users { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Category> categories = new List<Category>(){
                new Category { Id = 1, Name = "Product", NormalizedName = "product" },
                new Category { Id = 2, Name = "Technic", NormalizedName = "technic" } };

            List<Product> products = new List<Product>()
            {
                new Product{Id = 1, Name = "Product1", Description = "Description1", Price = 1,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image ="Item.png"},
                new Product{Id = 2, Name = "Product2", Description = "Description2", Price = 2,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image ="Item.png"},
                new Product{Id = 3, Name = "Technic1", Description = "Description3", Price = 3,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image ="Item.png"},
                new Product{Id = 4, Name = "Technic2", Description = "Description4", Price = 4,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image ="Item.png"},
                new Product{Id = 5, Name = "Product3", Description = "Description5", Price = 5,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image ="Item.png"},
                new Product{Id = 6, Name = "Product4", Description = "Description6", Price = 6,
                    Category = categories.Find(p => p.NormalizedName == "product"), Image = "Item.png"},
                new Product{Id = 7, Name = "Technic3", Description = "Description7", Price = 7,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image = "Item.png"},
                new Product{Id = 8, Name = "Technic4", Description = "Description8", Price = 8,
                    Category = categories.Find(p => p.NormalizedName == "technic"), Image ="Item.png"},
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
        }
    }
}
