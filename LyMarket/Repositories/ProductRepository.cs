using LyMarket.Data;
using LyMarket.Mapper;
using LyMarket.Models;
using LyMarket.Services.Internals.ProductServices.Dto;
using LyMarket.Services.ProductServices.Dto;

namespace LyMarket.Repositories;

public class ProductRepository(LyMarketDbContext context, ILogger logger) : GenericRepository<Product>(context, logger), IProductRepository
{
    private readonly LyMarketDbContext _context = context;

    public async Task<ProductResponse> CreateProduct(CreateProductRequest request, string? imageUrl)
    {
        try
        {
            var newProduct = request.MapCreateProductRequestToEntity();

            if (imageUrl is not null)
            {
                newProduct.ImageUrl = imageUrl;
            }
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.ToProductResponse();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} All method error", typeof(ProductRepository));
            throw;
        }
    }
}
