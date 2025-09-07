using Florin_Back.Exceptions.User;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Florin_Back.Services;

public class UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository) : IUserService
{
    public async Task<User> CreateUserAsync(User user)
    {
        if (await UserExistsAsync(user.Username, user.Email))
        {
            throw new UserAlreadyExistsException();
        }

        user.Password = passwordHasher.HashPassword(user, user.Password);

        return await userRepository.CreateUserAsync(user);
    }

    public async Task<bool> UserExistsAsync(string username, string email)
    {
        return await userRepository.UserExistsAsync(username, email);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userRepository.GetUserByEmailAsync(email);
    }

    public bool VerifyPassword(User user, string password)
    {
        return passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
    }

    public async Task<User> GetUserByIdAsync(long id)
    {
        return await userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException();
    }
}
