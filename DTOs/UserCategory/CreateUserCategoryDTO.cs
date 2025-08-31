using System.ComponentModel.DataAnnotations;

namespace Florin_Back.DTOs.Category;

public class CreateUserCategoryDTO
{
    [MinLength(3)]
    [MaxLength(50)]
    public required string Name { get; set; }
}
