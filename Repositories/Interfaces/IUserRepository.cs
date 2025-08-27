using Florin_Back.Models;

namespace Florin_Back.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<bool> UserExistsAsync(string username, string email);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(long id);
}
