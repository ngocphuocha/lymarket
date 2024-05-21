using LyMarket.Helpers.Pagination;
using LyMarket.Services.ProductServices;
using LyMarket.Services.ProductServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LyMarket.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(ProductServices productServices) : ControllerBase
{
    private readonly ProductServices _productServices = productServices;

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] RequestParameters request)
    {
        try
        {
            var products = await _productServices.GetProducts(request);
            return Ok(new ResponsePaginate<ProductResponse>(products, products.MetaData, "Get products successfully"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        try
        {
            var result = await _productServices.CreateProduct(request);
            return Ok(new ResponseItem<ProductResponse>
            {
                Status = RequestStatus.Success,
                Message = "Create new product successfully",
                Data = result
            });
        }
        catch (Exception e)
        {
            return BadRequest(new ResponseError
            {
                Status = RequestStatus.Success,
                Message = "Failed to create new product",
                Detail = e.Message
            });
        }
    }
}
