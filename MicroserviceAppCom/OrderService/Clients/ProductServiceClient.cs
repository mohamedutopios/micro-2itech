
namespace OrderService.Clients
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;

        public ProductServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:7000");

        }

        public async Task<bool> ProductExistAsync(int productId)
        {
            var reponse = await _httpClient.GetAsync($"/api/products/{productId}");
            return reponse.IsSuccessStatusCode;


        }
    }
}
