using Florin_Back.Models;

namespace Florin_Back.Services.Interfaces;

public interface IRefreshTokenService
{
    public Task<RefreshToken> CreateRefreshTokenAsync(User user);
    public Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token);
    public Task RevokeRefreshTokenAsync(RefreshToken refreshToken);
}
