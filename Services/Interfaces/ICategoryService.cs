using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;

namespace Florin_Back.Services.Interfaces;

public interface ICategoryService
{
    public Task<Pagination<Category>> GetUserCategoriesAsync(PaginationFilters pagination, CategoryFilters filters);
    public Task<Category?> GetUserCategoryAsync(long categoryId);
    public Task<Category> CreateUserCategoryAsync(Category category);
    public Task<Category> UpdateUserCategoryAsync(long categoryId, Category category);
    public Task DeleteUserCategoryAsync(long categoryId);
}
