using Florin_Back.Exceptions.Auth;
using Florin_Back.Exceptions.Category;
using Florin_Back.Exceptions.RefreshToken;
using Florin_Back.Exceptions.Transaction;
using Florin_Back.Exceptions.User;

namespace Florin_Back.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new
        {
            exception.Message,
            Type = exception.GetType().Name
        };

        context.Response.StatusCode = exception switch
        {
            BadCredentialsException => StatusCodes.Status401Unauthorized,
            InvalidRefreshTokenException => StatusCodes.Status403Forbidden,
            RefreshTokenNotFoundException or CategoryNotFoundException or TransactionNotFoundException or TransactionTypeNotFoundException => StatusCodes.Status404NotFound,
            UserAlreadyExistsException or CategoryAlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
