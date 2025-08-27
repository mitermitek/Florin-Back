using Florin_Back.Data;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Repositories;

public class RefreshTokenRepository(FlorinDbContext ctx) : IRefreshTokenRepository
{
    public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        ctx.RefreshTokens.Add(refreshToken);
        await ctx.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
    {
        return await ctx.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
    {
        refreshToken.RevokedAt = DateTime.UtcNow;
        ctx.RefreshTokens.Update(refreshToken);
        await ctx.SaveChangesAsync();
    }
}
