namespace Florin_Back.Exceptions.Category;

public class CategoryAlreadyExistsException : Exception
{
    public CategoryAlreadyExistsException() : base("Category with the same name already exists.") { }
    public CategoryAlreadyExistsException(string message) : base(message) { }
    public CategoryAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
}
