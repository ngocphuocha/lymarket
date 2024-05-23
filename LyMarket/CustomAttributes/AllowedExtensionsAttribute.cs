using System.ComponentModel.DataAnnotations;

namespace LyMarket.CustomAttributes;

public class AllowedExtensionsAttribute(string[] extensions) : ValidationAttribute
{

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }
        if (value is not IFormFile file) return new ValidationResult(GetErrorMessage());
        var extension = Path.GetExtension(file.FileName);
        return extensions.Contains(extension.ToLower())
            ? ValidationResult.Success
            : new ValidationResult(GetErrorMessage());
    }

    private string GetErrorMessage() => $"The file only except file extensions is {string.Join(", ", extensions)}";
}
