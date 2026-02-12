using ReportingService.Models;

namespace ReportingService.Services
{
    public class ReportService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ReportService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<List<OrderReport>> GetOrderReports()
        {
            var productUrl = _config["ServiceUrls:ProductService"];
            var orderUrl = _config["ServiceUrls:OrderService"];

            var products =
                await _httpClient.GetFromJsonAsync<List<Product>>
                ($"{productUrl}/api/products");

            var orders =
                await _httpClient.GetFromJsonAsync<List<Order>>
                ($"{orderUrl}/api/orders");

            var report =
                (from o in orders
                 join p in products
                 on o.ProductId equals p.Id
                 select new OrderReport
                 {
                     OrderId = o.Id,
                     ProductName = p.Name,
                     Quantity = o.Quantity,
                     Price = p.Price
                 }).ToList();

            return report;
        }
    }
}
