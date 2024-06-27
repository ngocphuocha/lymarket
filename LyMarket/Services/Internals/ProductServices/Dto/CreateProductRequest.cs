using System.ComponentModel.DataAnnotations;
using LyMarket.CustomAttributes;
using LyMarket.Enums;

namespace LyMarket.Services.Internals.ProductServices.Dto;

public class CreateProductRequest
{
    [Required]
    public string Name { get; set; }

    [AllowedExtensions([".jpg", ".jpeg", ".png"])]
    [MaxFileSizeKilobyte(2048)]
    public IFormFile? FileUpload { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StorageProvider StorageProvider { get; set; }
}
