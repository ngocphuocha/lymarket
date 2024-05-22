using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using LyMarket.Constants;
using LyMarket.Contracts;
using LyMarket.Helpers;
using LyMarket.Options;
using Microsoft.Extensions.Options;

namespace LyMarket.Services.External;

public class AwsS3Service : IStorageService
{
    private readonly ILogger _logger;
    private readonly AwsS3Options _options;
    private readonly IAmazonS3 _s3Client;

    public AwsS3Service(IOptions<AwsS3Options> options, ILogger logger)
    {
        _logger = logger;
        _options = options.Value;
        _s3Client = new AmazonS3Client(_options.AccessKeyId, _options.SecretAccessKey, RegionEndpoint.GetBySystemName(_options.Region));
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        try
        {
            var fileName = $"{Folders.Products}/{FileHelper.GenerateUniqueFileName(file)}";

            await using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = fileName,
                InputStream = stream
            };

            await _s3Client.PutObjectAsync(request);

            var imageUrl = $"{_options.BaseUrlObject}{fileName}";
            return imageUrl;
        }
        catch (AmazonS3Exception ex)
        {
            if (ex.ErrorCode is "InvalidAccessKeyId" or "SignatureDoesNotMatch")
            {
                _logger.LogError("Invalid AWS credentials.");
            }
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error uploading file to S3: {FileName}", file.FileName);
            Console.WriteLine(e);
            throw;
        }
    }

    public Task DeleteFileAsync(string fileName) => throw new NotImplementedException();
}
