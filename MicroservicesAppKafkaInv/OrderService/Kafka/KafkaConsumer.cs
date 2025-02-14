using Confluent.Kafka;
using OrderService.Services;

namespace OrderService.Kafka
{
    public class KafkaConsumer
    {
        private readonly IOrdersService _ordersService;
        private readonly string _topic = "customer-deleted";
        private readonly string _groupId = "order-service-group";
        private readonly string _bootstrapServers = "localhost:9092";

        public KafkaConsumer(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public void StartConsuming()
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(_topic);

            Console.WriteLine($"Listening for messages on topic: {_topic}");

            while (true)
            {
                var consumeResult = consumer.Consume();

                if (consumeResult != null)
                {
                    var customerId = int.Parse(consumeResult.Message.Key);
                    Console.WriteLine($"Received deletion event for Customer ID: {customerId}");

                    // Supprime les commandes associées à ce client
                    _ordersService.DeleteOrdersByCustomerIdAsync(customerId).Wait();
                }
            }
        }
    }
}

