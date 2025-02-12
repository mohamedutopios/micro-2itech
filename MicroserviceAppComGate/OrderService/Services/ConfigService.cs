using OrderService.Configurations;
using System.Text.Json;

namespace OrderService.Services
{
    public class ConfigService
    {
        private readonly HttpClient _httpClient;

        public ConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MongoDbSettings> GetMongoDbSettingsAsync(string serviceName)
        {
            var response = await _httpClient.GetAsync($"{serviceName}/default");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var settings = new MongoDbSettings();

            using (var document = JsonDocument.Parse(content))
            {
                var root = document.RootElement;

                if (root.TryGetProperty("propertySources", out JsonElement propertySources))
                {
                    var firstSource = propertySources[0].GetProperty("source");

                   
                    if (firstSource.TryGetProperty("MongoDbSettings.ConnectionString", out JsonElement connectionString))
                    {
                        settings.ConnectionString = connectionString.GetString() ?? string.Empty;
                    }

               
                    if (firstSource.TryGetProperty("MongoDbSettings.DatabaseName", out JsonElement databaseName))
                    {
                        settings.DatabaseName = databaseName.GetString() ?? string.Empty;
                    }
                }
            }

            return settings;
        }
    }
}

