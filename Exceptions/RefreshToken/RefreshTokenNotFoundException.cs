namespace Florin_Back.Exceptions.RefreshToken;

public class RefreshTokenNotFoundException : Exception
{
    public RefreshTokenNotFoundException() : base("Refresh token not found.") { }
    public RefreshTokenNotFoundException(string message) : base(message) { }
    public RefreshTokenNotFoundException(string message, Exception inner) : base(message, inner) { }
}
