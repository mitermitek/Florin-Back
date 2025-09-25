using Florin_Back.Exceptions.Category;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services.Interfaces;

namespace Florin_Back.Services;

public class CategoryService(IUserContextService userContextService, ICategoryRepository categoryRepository) : ICategoryService
{
    public Task<Pagination<Category>> GetUserCategoriesAsync(PaginationFilters pagination, CategoryFilters filters)
    {
        var userId = userContextService.GetUserId();

        return categoryRepository.GetCategoriesByUserIdAsync(userId, pagination, filters);
    }

    public async Task<Category?> GetUserCategoryAsync(long categoryId)
    {
        var userId = userContextService.GetUserId();
        var category = await categoryRepository.GetCategoryByUserIdAsync(categoryId, userId) ?? throw new CategoryNotFoundException();

        return category;
    }

    public async Task<Category> CreateUserCategoryAsync(Category category)
    {
        var userId = userContextService.GetUserId();
        var categoryExists = await categoryRepository.CategoryExistsByNameAndUserIdAsync(category.Name, userId);
        if (categoryExists)
        {
            throw new CategoryAlreadyExistsException();
        }

        category.UserId = userId;

        return await categoryRepository.CreateCategoryAsync(category);
    }

    public async Task<Category> UpdateUserCategoryAsync(long categoryId, Category category)
    {
        var userId = userContextService.GetUserId();
        var existingCategory = await categoryRepository.GetCategoryByUserIdAsync(categoryId, userId) ?? throw new CategoryNotFoundException();

        if (!string.Equals(existingCategory.Name, category.Name, StringComparison.OrdinalIgnoreCase))
        {
            var categoryExists = await categoryRepository.CategoryExistsByNameAndUserIdAsync(category.Name, userId);
            if (categoryExists)
            {
                throw new CategoryAlreadyExistsException();
            }

            existingCategory.Name = category.Name;
        }

        return await categoryRepository.UpdateCategoryAsync(existingCategory);
    }

    public async Task DeleteUserCategoryAsync(long categoryId)
    {
        var userId = userContextService.GetUserId();
        var category = await categoryRepository.GetCategoryByUserIdAsync(categoryId, userId) ?? throw new CategoryNotFoundException();

        await categoryRepository.DeleteCategoryAsync(category);
    }
}
