namespace LyMarket.Helpers.Pagination;

public record ResponsePaginate<T>(List<T> Data, MetaData MetaData, string Message) where T : class;
