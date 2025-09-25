using AutoMapper;
using Florin_Back.Models.DTOs.Category;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_Back.Controllers.Users
{
    [Route("api/User/[controller]")]
    [Tags("User/Categories")]
    [ApiController]
    [Authorize]
    public class CategoriesController(IMapper mapper, ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PaginationFilters pagination, [FromQuery] CategoryFilters filters)
        {
            var categories = await categoryService.GetUserCategoriesAsync(pagination, filters);
            var categoriesDTO = mapper.Map<Pagination<CategoryDTO>>(categories);

            return Ok(categoriesDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            var createdCategory = await categoryService.CreateUserCategoryAsync(category);
            var createdCategoryDTO = mapper.Map<CategoryDTO>(createdCategory);

            return CreatedAtAction(nameof(GetCategory), new { id = createdCategoryDTO.Id }, createdCategoryDTO);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCategory([FromRoute] long id)
        {
            var category = await categoryService.GetUserCategoryAsync(id);
            var categoryDTO = mapper.Map<CategoryDTO>(category);

            return Ok(categoryDTO);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] long id, [FromBody] UpdateCategoryDTO categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            var updatedCategory = await categoryService.UpdateUserCategoryAsync(id, category);
            var updatedCategoryDTO = mapper.Map<CategoryDTO>(updatedCategory);

            return Ok(updatedCategoryDTO);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long id)
        {
            await categoryService.DeleteUserCategoryAsync(id);

            return NoContent();
        }
    }
}
