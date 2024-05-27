using LyMarket.Data;
using LyMarket.Mapper;
using LyMarket.Models;
using LyMarket.Services.Internals.ProductServices.Dto;
using LyMarket.Services.ProductServices.Dto;
using Microsoft.EntityFrameworkCore;

namespace LyMarket.Repositories;

public class ProductRepository(LyMarketDbContext context, ILogger logger) : GenericRepository<Product>(context, logger), IProductRepository
{
    private readonly LyMarketDbContext _context = context;

    public async Task<ProductResponse> CreateProduct(CreateProductRequest request, string? imageUrl)
    {
        try
        {
            var newProduct = request.MapCreateProductRequestToEntity();
            var exitedProduct = await context.Products.FirstAsync(p => p.Name == newProduct.Name);

            if(exitedProduct is not null)
            {
                throw new Exception("Product already existed");
            }

            if (imageUrl is not null)
            {
                newProduct.ImageUrl = imageUrl;
            }
            await _context.Products.AddAsync(newProduct);
            return newProduct.ToProductResponse();
        }
        catch (Exception e)
        {
            Logger.LogError(e, "{Repo} All method error", typeof(ProductRepository));
            throw;
        }
    }
}
