namespace Florin_Back.Models;

public class Pagination<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int Total { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}
