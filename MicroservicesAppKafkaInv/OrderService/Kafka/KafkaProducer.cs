using Confluent.Kafka;
using System.Text.Json;

namespace OrderService.Kafka
{
    public class KafkaProducer 
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public void ProduceOrderPlacedEvent(int productId, int quantity)
        {
            var orderEvent = JsonSerializer.Serialize(new { ProductId = productId, Quantity = quantity });
            _producer.Produce("order-placed", new Message<string, string> { Key = productId.ToString(), Value = orderEvent });
        }
    }

}
