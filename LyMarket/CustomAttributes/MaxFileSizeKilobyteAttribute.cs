using System.ComponentModel.DataAnnotations;

namespace LyMarket.CustomAttributes;

public class MaxFileSizeKilobyteAttribute(int maxFileSize) : ValidationAttribute
{

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;
        if (value is not IFormFile file) return new ValidationResult(GetErrorMessage());
        var fileSizeInKilobytes = file.Length / 1024.0;
        return fileSizeInKilobytes > maxFileSize ? new ValidationResult(GetErrorMessage()) : ValidationResult.Success;
    }

    private string GetErrorMessage() => $"Maximum allowed file size is {maxFileSize} bytes.";
}
