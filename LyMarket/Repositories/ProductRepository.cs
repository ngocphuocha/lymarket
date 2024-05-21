using LyMarket.Data;
using LyMarket.Helpers.Pagination;
using LyMarket.Mapper;
using LyMarket.Models;
using LyMarket.Services.ProductServices.Dto;
using LyMarket.Services.TodoServices.DTO;

namespace LyMarket.Repositories;

public class ProductRepository(LyMarketDbContext context, ILogger logger) : GenericRepository<Product>(context, logger), IProductRepository
{
    private readonly LyMarketDbContext _context = context;

    public async Task<ProductResponse> CreateProduct(CreateProductRequest request)
    {
        try
        {
            var newProduct = await _context.Products.AddAsync(request.MapCreateProductRequestToEntity());
            await _context.SaveChangesAsync();

            return newProduct.Entity.ToProductResponse();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} All method error", typeof(ProductRepository));
            throw;
        }
    }
}
