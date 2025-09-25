namespace Florin_Back.Models.DTOs.User;

public class UserDTO
{
    public long Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}
