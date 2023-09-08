using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using Web_153504_Bagrovets.API.Data;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;

namespace Web_153504_Bagrovets.API.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private AppDbContext _dbContext;
        private readonly int _maxPageSize = 20;
        public ProductService( AppDbContext appDbContext)  
        {
            _dbContext = appDbContext;
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo, int pageSize)
        {
            if (pageSize > _maxPageSize)
                pageSize = _maxPageSize;
            else if (pageSize ==  0)
                pageSize = 3;

            if (pageNo == 0)
                pageNo = 1;

            var query = _dbContext.Products.Include(p => p.Category).AsQueryable();
            var dataList = new ListModel<Product>();
            query = query.Where(d => categoryNormalizedName==null
            || d.Category.NormalizedName.Equals(categoryNormalizedName));
            // количество элементов в списке
            var count = query.Count();
            if (count==0)
            {
                return new ResponseData<ListModel<Product>>
                {
                    Data = dataList
                };
            }
            // количество страниц
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);
            if (pageNo > totalPages)
                return new ResponseData<ListModel<Product>>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No such page"
                };
            dataList.Items = await query.Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;
            var response = new ResponseData<ListModel<Product>>
            {
                Data = dataList
            };
            return response;
        }
        public Task UpdateProductAsync(int id, Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}
