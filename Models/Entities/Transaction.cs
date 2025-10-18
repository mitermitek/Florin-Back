using System.ComponentModel.DataAnnotations.Schema;
using Florin_Back.Enums;

namespace Florin_Back.Models.Entities;

public class Transaction
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CategoryId { get; set; }
    public TransactionType Type { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
}
