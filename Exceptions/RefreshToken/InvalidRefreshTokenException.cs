namespace Florin_Back.Exceptions.RefreshToken;

public class InvalidRefreshTokenException : Exception
{
    public InvalidRefreshTokenException() : base("The provided refresh token is invalid or has expired.") { }
    public InvalidRefreshTokenException(string message) : base(message) { }
    public InvalidRefreshTokenException(string message, Exception inner) : base(message, inner) { }
}
