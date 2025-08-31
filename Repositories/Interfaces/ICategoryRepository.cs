using Florin_Back.Models;

namespace Florin_Back.Repositories.Interfaces;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(long userId);
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<Category?> GetCategoryByIdAndUserIdAsync(long id, long userId);
    public Task<Category> UpdateCategoryAsync(Category category);
    public Task DeleteCategoryAsync(Category category);
    public Task<Category?> GetCategoryByNameAndUserIdAsync(string name, long userId);
}
