using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Florin_Back.Exceptions.Auth;
using Florin_Back.Exceptions.RefreshToken;
using Florin_Back.Models;
using Florin_Back.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Florin_Back.Services;

public class AuthService(IConfiguration configuration, IUserService userService, IRefreshTokenService refreshTokenService) : IAuthService
{
    public async Task<User> RegisterAsync(User user)
    {
        return await userService.CreateUserAsync(user);
    }

    public async Task<User> LoginAsync(User user)
    {
        User? existingUser = await userService.GetUserByEmailAsync(user.Email);
        if (existingUser == null || !userService.VerifyPassword(existingUser, user.Password))
        {
            throw new BadCredentialsException();
        }

        return existingUser;
    }

    public string GenerateAccessToken(User user)
    {
        return GenerateJwtToken(user);
    }

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(user);
        return refreshToken.Token;
    }

    public async Task<string> RefreshAccessTokenAsync(string refreshToken)
    {
        var existingRefreshToken = await refreshTokenService.GetRefreshTokenByTokenAsync(refreshToken);
        if (existingRefreshToken == null || existingRefreshToken.RevokedAt != null || existingRefreshToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new InvalidRefreshTokenException();
        }

        var user = await userService.GetUserByIdAsync(existingRefreshToken.UserId) ?? throw new InvalidRefreshTokenException();

        return GenerateJwtToken(user);
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var existingRefreshToken = await refreshTokenService.GetRefreshTokenByTokenAsync(refreshToken);
        if (existingRefreshToken == null || existingRefreshToken.RevokedAt != null || existingRefreshToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new InvalidRefreshTokenException();
        }

        await refreshTokenService.RevokeRefreshTokenAsync(existingRefreshToken);
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSecret = configuration.GetValue<string>("JWT:Secret");
        var jwtValidIssuer = configuration.GetValue<string>("JWT:ValidIssuer");
        var jwtValidAudience = configuration.GetValue<string>("JWT:ValidAudience");

        if (string.IsNullOrEmpty(jwtSecret) || string.IsNullOrEmpty(jwtValidIssuer) || string.IsNullOrEmpty(jwtValidAudience))
        {
            throw new InvalidOperationException("JWT configuration is missing.");
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwtValidIssuer,
            audience: jwtValidAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
