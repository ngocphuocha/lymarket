namespace LyMarket.Contracts;

public interface IStorageService
{
    Task<string> UploadFileAsync(IFormFile file);
    Task DeleteFileAsync(string fileName);
}
