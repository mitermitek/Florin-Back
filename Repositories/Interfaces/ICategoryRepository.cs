using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;

namespace Florin_Back.Repositories.Interfaces;

public interface ICategoryRepository
{
    public Task<Pagination<Category>> GetCategoriesByUserIdAsync(long userId, PaginationFilters pagination, CategoryFilters filters);
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<Category?> GetCategoryByUserIdAsync(long id, long userId);
    public Task<Category> UpdateCategoryAsync(Category category);
    public Task DeleteCategoryAsync(Category category);
    public Task<bool> CategoryExistsByNameAndUserIdAsync(string name, long userId);
}
