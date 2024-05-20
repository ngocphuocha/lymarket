namespace LyMarket.Helpers.Pagination;

public record ResponseListItem<T> : ResponseBase where T : class
{
    public List<T> Data { get; init; } = new();
}
