using Microsoft.AspNetCore.Mvc;

namespace LyMarket.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts() => Ok("Hello Phuoc");
}
