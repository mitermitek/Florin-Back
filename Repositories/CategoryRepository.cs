using Florin_Back.Data;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;
using Florin_Back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Repositories;

public class CategoryRepository(FlorinDbContext ctx) : ICategoryRepository
{
    public async Task<Pagination<Category>> GetCategoriesByUserIdAsync(long userId, PaginationFilters pagination, CategoryFilters filters)
    {
        var page = pagination.Page;
        var size = pagination.Size;

        var query = ctx.Categories.Where(c => c.UserId == userId).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            query = query.Where(c => c.Name.Contains(filters.Search));
        }

        query = query.OrderBy(c => c.Name).AsNoTracking();

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();

        return new Pagination<Category>
        {
            Items = items,
            Total = total,
            Page = page,
            Size = size
        };
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        ctx.Categories.Add(category);
        await ctx.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetCategoryByUserIdAsync(long id, long userId)
    {
        return await ctx.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        ctx.Categories.Update(category);
        await ctx.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        ctx.Categories.Remove(category);
        await ctx.SaveChangesAsync();
    }

    public async Task<bool> CategoryExistsByNameAndUserIdAsync(string name, long userId)
    {
        return await ctx.Categories.AnyAsync(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase) && c.UserId == userId);
    }
}
