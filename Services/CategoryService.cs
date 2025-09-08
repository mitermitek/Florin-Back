using Florin_Back.Exceptions.Category;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services.Interfaces;

namespace Florin_Back.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public Task<IEnumerable<Category>> GetUserCategoriesAsync(long userId)
    {
        return categoryRepository.GetCategoriesByUserIdAsync(userId);
    }

    public async Task<Category?> GetUserCategoryByIdAsync(long userId, long categoryId)
    {
        var category = await categoryRepository.GetCategoryByIdAndUserIdAsync(categoryId, userId) ?? throw new CategoryNotFoundException();
        return category;
    }

    public async Task<Pagination<Category>> GetUserCategoriesAsync(long userId, int page, int size, CategoryFilters filters)
    {
        return await categoryRepository.GetCategoriesByUserIdAsync(userId, page, size, filters);
    }

    public async Task<Category> CreateUserCategoryAsync(long userId, Category category)
    {
        // check if category with the same name already exists for the user
        var existingCategory = await categoryRepository.GetCategoryByNameAndUserIdAsync(category.Name, userId);
        if (existingCategory is not null)
        {
            throw new CategoryAlreadyExistsException();
        }

        category.UserId = userId;
        return await categoryRepository.CreateCategoryAsync(category);
    }

    public async Task<Category> UpdateUserCategoryAsync(long userId, long categoryId, Category category)
    {
        // check if category exists
        var existingCategory = await categoryRepository.GetCategoryByIdAndUserIdAsync(categoryId, userId) ?? throw new CategoryNotFoundException();

        // check if category with the same name already exists for the user (exclude the current category)
        var categoryWithSameName = await categoryRepository.GetCategoryByNameAndUserIdAsync(category.Name, userId);
        if (categoryWithSameName is not null && categoryWithSameName.Id != categoryId)
        {
            throw new CategoryAlreadyExistsException();
        }

        existingCategory.Name = category.Name;
        return await categoryRepository.UpdateCategoryAsync(existingCategory);
    }

    public async Task DeleteUserCategoryAsync(long userId, long categoryId)
    {
        var category = await categoryRepository.GetCategoryByIdAndUserIdAsync(categoryId, userId) ?? throw new CategoryNotFoundException();

        await categoryRepository.DeleteCategoryAsync(category);
    }
}
