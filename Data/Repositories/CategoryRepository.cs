using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IstimAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(x => x.Id == categoryId);
        }
    }
}