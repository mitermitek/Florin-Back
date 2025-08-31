using Florin_Back.Enums;

namespace Florin_Back.DTOs.UserTransaction;

public class UpdateUserTransactionDTO
{
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public long CategoryId { get; set; }
}
