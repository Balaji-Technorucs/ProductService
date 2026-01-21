using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> Orders = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return NotFound($"Order with id {id} not found");

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateOrderRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ProductName))
                return BadRequest("Product name is required");

            var order = new Order
            {
                Id = Orders.Count + 1,
                ProductName = request.ProductName,
                Quantity = request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            Orders.Add(order);

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
    }

    // Simple model (keep it in same file for now)
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateOrderRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}

