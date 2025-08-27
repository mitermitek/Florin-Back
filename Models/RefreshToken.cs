using System.ComponentModel.DataAnnotations.Schema;

namespace Florin_Back.Models;

public class RefreshToken
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public required string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }
}
