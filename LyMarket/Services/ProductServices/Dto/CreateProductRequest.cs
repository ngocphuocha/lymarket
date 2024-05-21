using System.ComponentModel.DataAnnotations;

namespace LyMarket.Services.ProductServices.Dto;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }
}
