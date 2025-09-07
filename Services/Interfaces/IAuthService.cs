using Florin_Back.Models;

namespace Florin_Back.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(User user);
    Task<User> LoginAsync(User user);
    Task LogoutAsync();
    Task<User> GetCurrentUserAsync();
}
