using LyMarket.Contracts;

namespace LyMarket.Services.External;

public class BlobService: IStorageService
{

    public Task<string> UploadFileAsync(IFormFile file) => throw new NotImplementedException();
    public Task DeleteFileAsync(string fileName) => throw new NotImplementedException();
}
