using OrderService.DTO;

namespace OrderService.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:7000"); // URL en dur pour ProductService
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{productId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ProductDTO?> GetProductDetailsAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductDTO>();
            }
            return null;
        }
    }
}
