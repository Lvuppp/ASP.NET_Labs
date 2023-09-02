using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets_Lab1.Services.CategoryServices;

namespace Web_153504_Bagrovets_Lab1.Services.ProductSevices
{
    public class MemoryProductService : IProductService
    {
        private List<Product> _products;
        private List<Category> _categories;
        public MemoryProductService(ICategoryService categoryService) 
        {
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
            var response = new ResponseData<List<Product>>();
            response.Data = _products.Where(p => p.Category?.NormalizedName == categoryNormalizedName).ToList();
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
                new Product{Id = 1, Name = "Product1", Description = "Description1",
                    Category = _categories.Find(p => p.NormalizedName == "product")},
                new Product{Id = 2, Name = "Product2", Description = "Description2",
                    Category = _categories.Find(p => p.NormalizedName == "product")},
                new Product{Id = 3, Name = "Technic1", Description = "Description3",
                    Category = _categories.Find(p => p.NormalizedName == "technic")},
                new Product{Id = 4, Name = "Technic2", Description = "Description4",
                    Category = _categories.Find(p => p.NormalizedName == "technic")},
            };

        }
    }
}
