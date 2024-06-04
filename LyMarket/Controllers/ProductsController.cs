using LyMarket.Helpers.Pagination;
using LyMarket.Services.Internals.ProductServices;
using LyMarket.Services.Internals.ProductServices.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LyMarket.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(ProductServices productServices) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request)
    {
        try
        {
            var products = await productServices.GetProducts(request);
            return Ok(new ResponsePaginate<ProductResponse>(products, products.MetaData, "Get products successfully"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
    {
        try
        {
            var result = await productServices.CreateProduct(request);
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
                Status = RequestStatus.Fail,
                Message = "Failed to create new product",
                Detail = e.Message
            });
        }
    }
}
