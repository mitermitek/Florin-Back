using Florin_Back.Data;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Repositories;

public class CategoryRepository(FlorinDbContext ctx) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(long userId)
    {
        return await ctx.Categories.Where(c => c.UserId == userId).OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        ctx.Categories.Add(category);
        await ctx.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetCategoryByIdAndUserIdAsync(long id, long userId)
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

    public async Task<Category?> GetCategoryByNameAndUserIdAsync(string name, long userId)
    {
        return await ctx.Categories.FirstOrDefaultAsync(c => c.Name == name && c.UserId == userId);
    }
}
