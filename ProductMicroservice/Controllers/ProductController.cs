using Microsoft.AspNetCore.Mvc;

namespace ProductMicroservice.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[] { "Laptop", "Phone", "Tablet" });
        }
    }
}
