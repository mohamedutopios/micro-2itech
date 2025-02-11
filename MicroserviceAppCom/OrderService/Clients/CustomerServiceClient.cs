using OrderService.DTO;

namespace OrderService.Clients
{
    public class CustomerServiceClient : ICustomerServiceClient
    {
        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000"); // URL en dur pour CustomerService
        }

        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            var response = await _httpClient.GetAsync($"/api/customers/{customerId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<CustomerDTO?> GetCustomerDetailsAsync(int customerId)
        {
            var response = await _httpClient.GetAsync($"/api/customers/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDTO>();
            }
            return null;
        }
    }
}
