using LyMarket.Constants;
using LyMarket.Contracts;
using LyMarket.Data;
using LyMarket.Enums;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Services.Internals.ProductServices.Dto;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Services.Internals.ProductServices;

public class ProductServices(
    IUnitOfWork unitOfWork,
    [FromKeyedServices(nameof(StorageServiceName.AwsS3))]
    IStorageService awsS3Service,
    [FromKeyedServices(nameof(StorageServiceName.AzureBlob))]
    IStorageService azureBlobService)
{
    public async Task<PaginatedList<ProductResponse>> GetProducts(GetProductsRequest request)
    {
        var query = unitOfWork.Products.GetQueryAble();
        if (request.Search is { Length: > 0 })
        {
            var lowerCaseSearch = request.Search.Trim().ToLower();
            query = query.Where(c => EF.Functions.ILike(EF.Functions.Unaccent(c.Name.ToLower()),
                EF.Functions.Unaccent($"%{lowerCaseSearch}%")));
        }
        return await query.ToPaginatedList<ProductResponse>(request.PageNumber, request.PageSize);
    }
    public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        string? imageUrl = null;

        if (request.FileUpload is not null)
        {
            imageUrl = request.StorageProvider switch
            {
                StorageProvider.AwsS3 => await awsS3Service.UploadFileAsync(request.FileUpload),
                StorageProvider.AzureBlob => await azureBlobService.UploadFileAsync(request.FileUpload),
                _ => imageUrl
            };
        }
        var product = await unitOfWork.Products.CreateProduct(request, imageUrl);
        await unitOfWork.CompleteAsync();
        return product;
    }
}
