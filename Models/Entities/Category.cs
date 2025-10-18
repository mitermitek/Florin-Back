using System.ComponentModel.DataAnnotations.Schema;

namespace Florin_Back.Models.Entities;

public class Category
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];
}
