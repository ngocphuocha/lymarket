using System.ComponentModel.DataAnnotations;
using LyMarket.CustomAttributes;

namespace LyMarket.Services.Internals.ProductServices.Dto;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }

    [AllowedExtensions([".jpg", ".jpeg"])]
    [MaxFileSizeKilobyte(2048)]
    public IFormFile? FileUpload { get; set; }
}
