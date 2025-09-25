using System.ComponentModel.DataAnnotations;

namespace Florin_Back.Models.DTOs.Auth;

public class RegisterDTO
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string? Username { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string? Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}
