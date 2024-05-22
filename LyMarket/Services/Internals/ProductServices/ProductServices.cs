using LyMarket.Data;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Services.External;
using LyMarket.Services.Internals.ProductServices.Dto;
using LyMarket.Services.ProductServices.Dto;

namespace LyMarket.Services.Internals.ProductServices;

public class ProductServices(IUnitOfWork unitOfWork, AwsS3Service awsS3Service)
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
            imageUrl = await awsS3Service.UploadFileAsync(request.FileUpload);
        }
        var product = await unitOfWork.Products.CreateProduct(request, imageUrl);
        return product;
    }
}
