using Confluent.Kafka;

namespace CustomerService.Kafka
{
    public class KafkaProducer
    {

        private readonly IProducer<string,string> _producer;

        public KafkaProducer()
        {

            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<string,string>(config).Build();

        }

        
        public async Task sendMessageAsync(string topic, string key, string message)
        {
            await _producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = message });
            Console.WriteLine($"Messafe sent to kafka : {message}");
        }


    }
}
