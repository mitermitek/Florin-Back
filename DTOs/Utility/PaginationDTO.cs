namespace Florin_Back.DTOs.Utility;

public class PaginationDTO<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int Total { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public int PageCount => (int)Math.Ceiling((double)Total / Size);
    public bool HasNextPage => Page < PageCount;
    public bool HasPreviousPage => Page > 1;
}
