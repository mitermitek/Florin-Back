using Florin_Back.Models;

namespace Florin_Back.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    public Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken);
    public Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    public Task RevokeRefreshTokenAsync(RefreshToken refreshToken);
}
