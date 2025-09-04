using Florin_Back.Data;
using Florin_Back.Mappings;
using Florin_Back.Middlewares;
using Florin_Back.Models;
using Florin_Back.Repositories;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(opt =>
{
    var frontEndURL = builder.Configuration.GetValue<string>("CORS:FrontEndURL");
    if (string.IsNullOrEmpty(frontEndURL))
    {
        throw new InvalidOperationException("CORS configuration is missing.");
    }

    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(frontEndURL)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<FlorinDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.HttpOnly = true;
        opt.Cookie.SameSite = SameSiteMode.Strict;
        opt.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        opt.Cookie.Name = "florin";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        opt.SlidingExpiration = true;
        opt.Cookie.IsEssential = true;
        opt.Cookie.MaxAge = TimeSpan.FromDays(7);

        opt.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
        opt.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// global error handler
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
