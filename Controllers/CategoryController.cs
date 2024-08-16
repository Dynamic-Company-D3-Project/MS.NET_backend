using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Entities;
using WEBAPI.EntityDTOs;

namespace WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class CategoryController : ControllerBase
    {
        private ProjectContext DBContext;

        public CategoryController(ProjectContext dbContext)
        {
            DBContext = dbContext;
        }

        [HttpGet("getSubcategories")]
        public async Task<ActionResult<SubCategoryDTO>> GetSubcategory()
        {
            var subcategories = await DBContext.Subcategories
                .Where(s=> s.IsVisible ==1)
            .Select(s => new SubCategoryDTO
            {
                Id = s.Id,
                CategoryName = s.CategoryName,
                Description = s.Description,
                Image = s.Image,
                IsVisible = s.IsVisible,
                LastUpdated = s.LastUpdated,
                Price = s.Price,
                Rating = s.Rating,
                CategoryId = s.CategoryId
            })
            .ToListAsync();

            return Ok(subcategories);
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryDTO>> AddSubcategory([FromBody] SubCategoryDTO subcategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategory = new Subcategory
            {
                CategoryName = subcategoryDto.CategoryName,
                Description = subcategoryDto.Description,
                Image = subcategoryDto.Image,
                IsVisible = subcategoryDto.IsVisible,
                LastUpdated = subcategoryDto.LastUpdated ?? DateTime.UtcNow,
                Price = subcategoryDto.Price,
                Rating = subcategoryDto.Rating,
                CategoryId = subcategoryDto.CategoryId
            };

            DBContext.Subcategories.Add(subcategory);
            await DBContext.SaveChangesAsync();

            return Ok(subcategory);
        }

        [HttpGet("subCategory/{id}")]
        public async Task<ActionResult<SubCategoryDTO>> GetSubcategoryById(int id)
        {
            var subcategory = await DBContext.Subcategories
                .Where(s => s.Id == id)
                .Select(s => new SubCategoryDTO
                {
                    Id = s.Id,
                    CategoryName = s.CategoryName,
                    Description = s.Description,
                    Image = s.Image,
                    IsVisible = s.IsVisible,
                    LastUpdated = s.LastUpdated,
                    Price = s.Price,
                    Rating = s.Rating,
                    CategoryId = s.CategoryId
                })
                .FirstOrDefaultAsync();

            if (subcategory == null)
            {
                return NotFound();
            }

            return Ok(subcategory);
        }

        [HttpGet()]
        public async Task<ActionResult<SubCategoryDTO>> GetALlCategories() {
            return Ok(DBContext.Categories.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubcategory(int id)
        {
            var subcategory = await DBContext.Subcategories.FindAsync(id);

            if (subcategory == null)
            {
                return NotFound(new { message = $"Subcategory with Id = {id} not found." });
            }
            subcategory.IsVisible = 0;
            //DBContext.Subcategories.Remove(subcategory);
            await DBContext.SaveChangesAsync();

            return Ok(new { message = $"Subcategory with Id = {id} deleted successfully." });
        }


    }
}
