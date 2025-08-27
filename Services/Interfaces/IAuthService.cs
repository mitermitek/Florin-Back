using Florin_Back.Models;

namespace Florin_Back.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(User user);
    Task<User> LoginAsync(User user);
    string GenerateAccessToken(User user);
    Task<string> GenerateRefreshTokenAsync(User user);
    Task<string> RefreshAccessTokenAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
}
