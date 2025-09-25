using System.ComponentModel.DataAnnotations;

namespace Florin_Back.Models.DTOs.Category;

public class UpdateCategoryDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string? Name { get; set; }
}
