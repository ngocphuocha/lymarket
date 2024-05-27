using LyMarket.Constants;
using LyMarket.Contracts;
using LyMarket.Data;
using LyMarket.Enums;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Services.External;
using LyMarket.Services.Internals.ProductServices.Dto;
using LyMarket.Services.ProductServices.Dto;

namespace LyMarket.Services.Internals.ProductServices;

public class ProductServices(IUnitOfWork unitOfWork, [FromKeyedServices(nameof(StorageServiceName.AwsS3))] IStorageService awsS3Service, [FromKeyedServices(nameof(StorageServiceName.AzureBlob))] IStorageService azureBlobService)
{
    public async Task<PaginatedList<ProductResponse>> GetProducts(RequestParameters request)
    {
        var query = unitOfWork.Products.GetQueryAble();
        return await query.ToPaginatedList<ProductResponse>(request.PageNumber, request.PageSize);
    }
    public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        string? imageUrl = null;

        if (request.FileUpload is not null)
        {
            imageUrl = request.StorageProvider switch
            {
                StogrageProvider.AwsS3 => await awsS3Service.UploadFileAsync(request.FileUpload),
                StogrageProvider.AzureBlob => await azureBlobService.UploadFileAsync(request.FileUpload),
                _ => imageUrl
            };
        }
        var product = await unitOfWork.Products.CreateProduct(request, imageUrl);
        await unitOfWork.CompleteAsync();
        return product;
    }
}
