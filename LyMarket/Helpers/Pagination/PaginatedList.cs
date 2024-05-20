namespace LyMarket.Helpers.Pagination;

public class PaginatedList<T> : List<T>
{
    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
        AddRange(items);
    }

    public MetaData MetaData { get; set; }

    // public static PaginatedList<T> ToPaginatedList(List<T> source, int pageNumber, int pageSize)
    // {
    //     var count = source.Count;
    //     var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    //     return new PaginatedList<T>(items, count, pageNumber, pageSize);
    // }
}
