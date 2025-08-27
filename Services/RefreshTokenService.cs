using Florin_Back.Exceptions.RefreshToken;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services.Interfaces;

namespace Florin_Back.Services;

public class RefreshTokenService(IRefreshTokenRepository refreshTokenRepository) : IRefreshTokenService
{
    public async Task<RefreshToken> CreateRefreshTokenAsync(User user)
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            User = user
        };

        return await refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenByTokenAsync(string token)
    {
        return await refreshTokenRepository.GetRefreshTokenByTokenAsync(token);
    }

    public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
    {
        await refreshTokenRepository.RevokeRefreshTokenAsync(refreshToken);
    }
}
