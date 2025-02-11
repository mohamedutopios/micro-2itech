
using System.Runtime.CompilerServices;

namespace OrderService.Clients
{
    public class CustomerServiceClient : ICustomerServiceClient
    {

        private readonly HttpClient _httpClient;

        public CustomerServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000");

        }

        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            var reponse = await _httpClient.GetAsync($"/api/customer/{customerId}");
            return reponse.IsSuccessStatusCode;
           
        }
    }
}
