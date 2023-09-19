using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Drawing.Printing;
using Web_153504_Bagrovets.API.Data;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Web_153504_Bagrovets.API.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private AppDbContext _dbContext;
        private readonly int _maxPageSize = 20;
        private HttpContext _httpContext;
        private string _imagePath;
        public ProductService( AppDbContext appDbContext, IWebHostEnvironment env,
            IHttpContextAccessor accessor)  
        {
            _dbContext = appDbContext;
            _imagePath = Path.Combine(env.WebRootPath, "images");
            _httpContext = accessor.HttpContext; 
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return;
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();
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
        public async Task UpdateProductAsync(int id, Product product)
        {
            var productTmp = await _dbContext.Products.FindAsync(id);
            _dbContext.Entry(productTmp).CurrentValues.SetValues(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ResponseData<Product>> CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return new ResponseData<Product> { Data = product };
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            var responseData = new ResponseData<string>();
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                responseData.Success = false;
                responseData.ErrorMessage = "No item found";
                return responseData;
            }

            var host = "https://"+ _httpContext.Request.Host;

            if (formFile != null)
            {
                // Удалить предыдущее изображение
                if (!String.IsNullOrEmpty(product.Image))
                {
                    var prevImage = Path.Combine(_imagePath,Path.GetFileName(product.Image));
                    try
                    {
                        if(File.Exists(prevImage))
                            File.Delete(prevImage);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }

                }   

                // Создать имя файла
                var ext = Path.GetExtension(formFile.FileName);
                var fName = Path.ChangeExtension(Path.GetRandomFileName(), ext);

                // Сохранить файл
                try
                {
                    using (FileStream destStream = new FileStream(Path.Combine(_imagePath, fName), FileMode.Create))
                    {
                        destStream.CopyTo(destStream);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }

                // Указать имя файла в объекте
                product.Image = fName;
                await _dbContext.SaveChangesAsync();
            }
            responseData.Data = product.Image;
            return responseData;
        }
    }
}
