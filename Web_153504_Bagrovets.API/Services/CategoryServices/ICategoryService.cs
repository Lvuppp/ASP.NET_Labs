using Web_153504_Bagrovets.Domain.Entities;
using Web_153504_Bagrovets_Lab1.Models;

namespace Web_153504_Bagrovets.API.Data.CategoryServices
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
