using System.Collections.Generic;
using System.Threading.Tasks;
using IstimAPI.Data.IRepositories;
using IstimAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace IstimAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            if (!CategoryExists(id))
                return NotFound(new { Message = "Esta categoria não existe" });

            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _categoryRepository.CreateCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível criar esta categoria" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] Category category)
        {
            if (!CategoryExists(id))
                return NotFound(new { Message = "Esta categoria não existe" });

            if (id != category.Id)
                return NotFound(new { Message = "Categoria não encontrada" });

            try
            {
                await _categoryRepository.UpdateCategoryAsync(category);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível atualizar esta categoria" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!CategoryExists(id))
                return NotFound(new { Message = "Esta categoria não existe" });

            try
            {
                await _categoryRepository.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch
            {
                return BadRequest(new { Message = "Não foi possível remover esta categoria" });
            }
        }

        private bool CategoryExists(int categoryId)
        {
            return _categoryRepository.CategoryExists(categoryId);
        }
    }
}