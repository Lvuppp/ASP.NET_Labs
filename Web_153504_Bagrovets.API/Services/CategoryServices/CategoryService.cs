using Microsoft.EntityFrameworkCore;
using Web_153504_Bagrovets.API.Data;
using Web_153504_Bagrovets.API.Data.CategoryServices;
using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;

namespace Web_153504_Bagrovets.API.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private AppDbContext _context = null!;
        public CategoryService(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var query = _context.Categories.AsQueryable();
            var dataList = new ListModel<Category>();
            return Task.FromResult(new ResponseData<List<Category>>
            {
                Data = query.ToList()
            });
        }

    }
}
