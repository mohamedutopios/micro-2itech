using OrderService.DTO;
using System.Text.Json;

namespace OrderService.Clients
{
    public class InventoryServiceClient : IInventoryServiceClient
    {
        private readonly HttpClient _httpClient;

        public InventoryServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<InventoryItemDTO> GetStock(int productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5005/api/inventory/{productId}");

            Console.WriteLine($"➡ HTTP GET {response.RequestMessage.RequestUri}");
            Console.WriteLine($"➡ Status Code: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Requête échouée");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Réponse brute : {content}");

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Réponse vide !");
                return null;
            }

            try
            {
                var result = JsonSerializer.Deserialize<InventoryItemDTO>(content);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de désérialisation : {ex.Message}");
                return null;
            }
        }

    }
}
