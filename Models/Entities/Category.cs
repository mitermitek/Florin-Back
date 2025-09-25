using System.ComponentModel.DataAnnotations.Schema;

namespace Florin_Back.Models.Entities;

public class Category
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}
