using Florin_Back.Data;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Repositories;

public class UserRepository(FlorinDbContext ctx) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user)
    {
        var entity = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<bool> UserExistsAsync(string username, string email)
    {
        return await ctx.Users.AnyAsync(u => u.Username == username || u.Email == email);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
