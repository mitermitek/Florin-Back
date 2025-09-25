namespace Florin_Back.Models.Utilities.Filters;

public class TransactionFilters
{
    public string? Search { get; set; }
    public string? Type { get; set; }
    public long? CategoryId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
}
