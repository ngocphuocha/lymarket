using LyMarket.Constants;
using LyMarket.Contracts;
using LyMarket.Data;
using LyMarket.Options;
using LyMarket.Services.External;

namespace LyMarket.Extensions;

public static class ExternalServiceExtension
{
    public static void AddExternalService(this IServiceCollection services)
    {
        services.AddDbContext<LyMarketDbContext>();

        services.Configure<AwsS3Options>(options =>
        {
            options.BucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME")!;
            options.Region = Environment.GetEnvironmentVariable("AWS_REGION")!;
            options.BaseUrlObject = Environment.GetEnvironmentVariable("AWS_BASE_URL_OBJECT")!;
            options.AccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID")!;
            options.SecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY")!;
        });

        services.AddKeyedSingleton<IStorageService, AwsS3Service>(StorageServiceName.AwsS3);
        services.AddKeyedSingleton<IStorageService, AzureBlobService>(StorageServiceName.AzureBlob);
    }
}
