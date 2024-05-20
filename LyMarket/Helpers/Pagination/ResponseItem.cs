namespace LyMarket.Helpers.Pagination;

public record ResponseItem<T> : ResponseBase where T : class
{
    public required T Data { get; set; }
}
