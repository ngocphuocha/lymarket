namespace LyMarket.Helpers.Pagination;

public enum RequestStatus
{
    Success,
    Fail
}

public abstract record ResponseBase
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RequestStatus Status { get; init; }

    public required string Message { get; init; }
}
