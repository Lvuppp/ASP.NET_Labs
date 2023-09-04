using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;

namespace Web_153504_Bagrovets_Lab1.Services.ProductSevices
{
    public class MemoryProductService : IProductService
    {
        private List<Product>? _products;
        private List<Category>? _categories;
        private readonly IConfiguration _config;

        public MemoryProductService([FromServices] IConfiguration config,
            ICategoryService categoryService,int pageNo = 2) 
        {
            _config = config;
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            SetupData();
        }

        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var response = new ResponseData<ListModel<Product>>();
            ListModel<Product> listModel = new ListModel<Product>();

            int itemsPerPage = (int)_config.GetValue(typeof(int),"ItemsPerPage");

            listModel.TotalPages = _products.Count() / itemsPerPage;
            if (_products.Count() % itemsPerPage != 0)
                listModel.TotalPages++;

            listModel.CurrentPage = pageNo;
            
            if (categoryNormalizedName is null)
                listModel.Items = _products.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            else
                listModel.Items = _products.Where(p => p.Category.NormalizedName.Equals(categoryNormalizedName)).
                    Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            response.Data = listModel;
            return Task.FromResult(response);
        }

        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private void SetupData()
        {
            _products = new List<Product>()
            {
                new Product{Id = 1, Name = "Product1", Description = "Description1", Price = 1,
                    Category = _categories.Find(p => p.NormalizedName == "product")},
                new Product{Id = 2, Name = "Product2", Description = "Description2", Price = 2,
                    Category = _categories.Find(p => p.NormalizedName == "product")},
                new Product{Id = 3, Name = "Technic1", Description = "Description3", Price = 3,
                    Category = _categories.Find(p => p.NormalizedName == "technic")},
                new Product{Id = 4, Name = "Technic2", Description = "Description4", Price = 4,
                    Category = _categories.Find(p => p.NormalizedName == "technic")},
                new Product{Id = 5, Name = "Product3", Description = "Description5", Price = 5,
                    Category = _categories.Find(p => p.NormalizedName == "product")},
                new Product{Id = 6, Name = "Product4", Description = "Description6", Price = 6,
                    Category = _categories.Find(p => p.NormalizedName == "product")},
                new Product{Id = 7, Name = "Technic3", Description = "Description7", Price = 7,
                    Category = _categories.Find(p => p.NormalizedName == "technic")},
                new Product{Id = 8, Name = "Technic4", Description = "Description8", Price = 8,
                    Category = _categories.Find(p => p.NormalizedName == "technic")},
            };

        }
    }
}
