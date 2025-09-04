using System.Security.Claims;
using Florin_Back.Exceptions.Auth;
using Florin_Back.Models;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Florin_Back.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor, IUserService userService) : IAuthService
{
    public async Task<User> RegisterAsync(User user)
    {
        return await userService.CreateUserAsync(user);
    }

    public async Task LoginAsync(User user)
    {
        User? existingUser = await userService.GetUserByEmailAsync(user.Email);
        if (existingUser == null || !userService.VerifyPassword(existingUser, user.Password))
        {
            throw new BadCredentialsException();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString())
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var authProperties = new AuthenticationProperties { };

        await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
    }

    public async Task LogoutAsync()
    {
        await httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
