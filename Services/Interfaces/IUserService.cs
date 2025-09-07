using Florin_Back.Models;

namespace Florin_Back.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task<bool> UserExistsAsync(string username, string email);
    Task<User?> GetUserByEmailAsync(string email);
    bool VerifyPassword(User user, string password);
    Task<User> GetUserByIdAsync(long id);
}
