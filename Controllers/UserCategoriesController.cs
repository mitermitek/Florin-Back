using AutoMapper;
using Florin_Back.DTOs.Category;
using Florin_Back.DTOs.UserCategory;
using Florin_Back.DTOs.Utility;
using Florin_Back.Models;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserCategoriesController(IUserContextService userContextService, IMapper mapper, ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserCategories([FromQuery] PaginationFiltersDTO pagination, [FromQuery] UserCategoryFiltersDTO filters)
        {
            var userId = userContextService.GetUserId();
            var categoryFilters = mapper.Map<CategoryFilters>(filters);
            var userCategories = await categoryService.GetUserCategoriesAsync(userId, pagination.Page, pagination.Size, categoryFilters);
            var userCategoriesDTO = mapper.Map<PaginationDTO<UserCategoryDTO>>(userCategories);

            return Ok(userCategoriesDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserCategory([FromBody] CreateUserCategoryDTO categoryDto)
        {
            var userId = userContextService.GetUserId();
            var mappedCategory = mapper.Map<Category>(categoryDto);
            var createdCategory = await categoryService.CreateUserCategoryAsync(userId, mappedCategory);
            var createdCategoryDto = mapper.Map<UserCategoryDTO>(createdCategory);

            return CreatedAtAction(nameof(GetUserCategory), new { id = createdCategoryDto.Id }, createdCategoryDto);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserCategory([FromRoute] long id)
        {
            var userId = userContextService.GetUserId();
            var category = await categoryService.GetUserCategoryByIdAsync(userId, id);
            var categoryDto = mapper.Map<UserCategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUserCategory([FromRoute] long id, [FromBody] UpdateUserCategoryDTO categoryDto)
        {
            var userId = userContextService.GetUserId();
            var mappedCategory = mapper.Map<Category>(categoryDto);
            var updatedCategory = await categoryService.UpdateUserCategoryAsync(userId, id, mappedCategory);
            var updatedCategoryDto = mapper.Map<UserCategoryDTO>(updatedCategory);

            return Ok(updatedCategoryDto);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUserCategory([FromRoute] long id)
        {
            var userId = userContextService.GetUserId();
            await categoryService.DeleteUserCategoryAsync(userId, id);

            return NoContent();
        }
    }
}
