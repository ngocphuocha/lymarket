using LyMarket.Helpers.Pagination;

namespace LyMarket.Services.Internals.ProductServices.Dto;

public class GetProductsRequest : RequestParameters
{
    public string? Search { get; set; }
}
