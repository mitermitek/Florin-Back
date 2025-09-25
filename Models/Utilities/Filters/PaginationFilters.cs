using System.ComponentModel.DataAnnotations;

namespace Florin_Back.Models.Utilities.Filters;

public class PaginationFilters
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; } = 1;

    [Range(1, 50, ErrorMessage = "Size must be between 1 and 50")]
    public int Size { get; set; } = 10;
}
