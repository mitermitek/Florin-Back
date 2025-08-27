using System.Text;
using Florin_Back.Data;
using Florin_Back.Mappings;
using Florin_Back.Models;
using Florin_Back.Repositories;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FlorinDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtValidIssuer = builder.Configuration.GetValue<string>("JWT:ValidIssuer");
    var jwtValidAudience = builder.Configuration.GetValue<string>("JWT:ValidAudience");
    var jwtSecret = builder.Configuration.GetValue<string>("JWT:Secret");

    if (string.IsNullOrEmpty(jwtSecret) || string.IsNullOrEmpty(jwtValidIssuer) || string.IsNullOrEmpty(jwtValidAudience))
    {
        throw new InvalidOperationException("JWT configuration is missing.");
    }

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtValidIssuer,
        ValidAudience = jwtValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// automapper configuration
builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddProfile<MappingProfile>();
});

// password hasher
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// user context service
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

// services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IUserService, UserService>();

// repositories
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
