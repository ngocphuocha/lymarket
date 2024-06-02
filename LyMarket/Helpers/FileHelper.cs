namespace LyMarket.Helpers;

public static class FileHelper
{
    public static string GenerateUniqueFileName(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        return $"{Guid.NewGuid()}{fileExtension}";
    }
}
