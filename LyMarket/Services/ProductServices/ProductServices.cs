using LyMarket.Data;
using LyMarket.Extensions;
using LyMarket.Helpers.Pagination;
using LyMarket.Services.ProductServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LyMarket.Services.ProductServices;

public class ProductServices(IUnitOfWork unitOfWork)
{
    public async Task<PaginatedList<ProductResponse>> GetProducts(RequestParameters request)
    {
        var query = unitOfWork.Products.GetQueryAble();
        return await query.ToPaginatedList<ProductResponse>(request.PageNumber, request.PageSize);
    }
    public async Task<ProductResponse> CreateProduct(CreateProductRequest request) => await unitOfWork.Products.CreateProduct(request);
}
