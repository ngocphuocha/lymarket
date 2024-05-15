using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LyMarket.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok("Hello World");
        }
    }
}
