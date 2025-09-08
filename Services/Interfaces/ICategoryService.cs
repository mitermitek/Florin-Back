using Florin_Back.Models;

namespace Florin_Back.Services.Interfaces;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetUserCategoriesAsync(long userId);
    public Task<Pagination<Category>> GetUserCategoriesAsync(long userId, int page, int size, CategoryFilters filters);
    public Task<Category?> GetUserCategoryByIdAsync(long userId, long categoryId);
    public Task<Category> CreateUserCategoryAsync(long userId, Category category);
    public Task<Category> UpdateUserCategoryAsync(long userId, long categoryId, Category category);
    public Task DeleteUserCategoryAsync(long userId, long categoryId);
}
