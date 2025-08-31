using System.ComponentModel.DataAnnotations;

namespace Florin_Back.DTOs.Category;

public class UpdateUserCategoryDTO
{
    [MinLength(3)]
    [MaxLength(50)]
    public required string Name { get; set; }
}
