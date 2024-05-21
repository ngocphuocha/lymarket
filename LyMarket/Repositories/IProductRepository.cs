using LyMarket.Models;
using LyMarket.Services.ProductServices.Dto;

namespace LyMarket.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<ProductResponse> CreateProduct(CreateProductRequest request);
}
