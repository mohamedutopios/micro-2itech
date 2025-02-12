using System.Text.Json;

namespace ProductService.Services
{
    public class ConfigService
    {

        private readonly HttpClient _httpClient;

        public ConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetConnectionStringAsync(string serviceName)
        {

            var response = await _httpClient.GetAsync($"{serviceName}/default");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            using (var document = JsonDocument.Parse(content))
            {
                var root = document.RootElement;

                if (root.TryGetProperty("propertySources", out JsonElement propertySources))
                {
                    var firstSource = propertySources[0].GetProperty("source");

                    if (firstSource.TryGetProperty("ConnectionStrings.DefaultConnection", out JsonElement defaultConnection))
                    {
                        return defaultConnection.GetString();
                    }
                }
            }

            return string.Empty;


        }


    }
}
