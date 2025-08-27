using System;
using System.ComponentModel.DataAnnotations;

namespace Florin_Back.DTOs.Auth;

public class LoginDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
