using Florin_Back.DTOs.Category;
using Florin_Back.Enums;

namespace Florin_Back.DTOs.UserTransaction;

public class UserTransactionDTO
{
    public long Id { get; set; }
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public UserCategoryDTO? Category { get; set; }
}
