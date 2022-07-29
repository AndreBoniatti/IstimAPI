using System.Collections.Generic;
using System.Threading.Tasks;
using IstimAPI.Models;

namespace IstimAPI.Data.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
        bool CategoryExists(int categoryId);
    }
}