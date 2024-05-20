namespace LyMarket.Helpers.Pagination;

public record ResponseError : ResponseBase
{
    public int? ErrorCode { get; init; }
    public string? Detail { get; init; }
}
