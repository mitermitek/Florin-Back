using Florin_Back.Enums;
using Florin_Back.Models.DTOs.Category;

namespace Florin_Back.Models.DTOs.Transaction;

public class TransactionDTO
{
    public long Id { get; set; }
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public CategoryDTO? Category { get; set; }
}
