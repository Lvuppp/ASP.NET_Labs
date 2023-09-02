using Web_153504_Bagrovets_Lab1.Models;
using Web_153504_Bagrovets.Domain.Entities;

namespace Web_153504_Bagrovets_Lab1.Services.CategoryServices
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
