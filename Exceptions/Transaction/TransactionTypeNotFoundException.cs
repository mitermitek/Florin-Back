namespace Florin_Back.Exceptions.Transaction;

public class TransactionTypeNotFoundException : Exception
{
    public TransactionTypeNotFoundException() : base("Transaction type not found.") { }
    public TransactionTypeNotFoundException(string message) : base(message) { }
    public TransactionTypeNotFoundException(string message, Exception inner) : base(message, inner) { }
}
