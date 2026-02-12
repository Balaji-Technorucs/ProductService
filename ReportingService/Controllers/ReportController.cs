using Microsoft.AspNetCore.Mvc;
using ReportingService.Services;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _service;

        public ReportController(ReportService service)
        {
            _service = service;
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok("Reporting Service is working.");
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderReports()
        {
            var data = await _service.GetOrderReports();
            return Ok(data);
        }
    }
}
