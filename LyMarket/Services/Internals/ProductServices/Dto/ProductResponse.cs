namespace LyMarket.Services.Internals.ProductServices.Dto;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
