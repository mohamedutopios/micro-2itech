using Confluent.Kafka;
using InventaireService.Models;
using InventaireService.Repositories;
using System.Text.Json;

namespace InventaireService.Services
{
    public class InventorysService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly IProducer<string, string> _producer;

        public InventorysService(IInventoryRepository repository)
        {
            _repository = repository;
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public List<InventoryItem> GetInventory() => _repository.GetInventory();
        public InventoryItem GetStock(int productId) => _repository.GetStock(productId);
        public void AddStock(InventoryItem item) => _repository.AddStock(item);
        public void UpdateStock(InventoryItem item)
        {
            _repository.UpdateStock(item);
           
        }
        public void DeleteStock(int productId) => _repository.DeleteStock(productId);
       
    }
}
