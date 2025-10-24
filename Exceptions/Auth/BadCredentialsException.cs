namespace Florin_Back.Exceptions.Auth;

public class BadCredentialsException : Exception
{
    public BadCredentialsException() : base("Invalid username or password.") { }
    public BadCredentialsException(string message) : base(message) { }
    public BadCredentialsException(string message, Exception inner) : base(message, inner) { }
}
