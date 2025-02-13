using Confluent.Kafka;
using OrderService.Services;

namespace OrderService.Kafka
{
    public class KafkaConsumer
    {


        private readonly IOrdersService _iorderService;
        private readonly string _topic = "customer-deleted";
        private readonly string _groupId = "order-service-group";
        private readonly string _bootstrapServers = "localhost:9092";

        public KafkaConsumer(IOrdersService ordersService)
        {
            _iorderService = ordersService;
        }


        public void StartConsuming()
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };


            using var consumer = new ConsumerBuilder<string,string>(config).Build();

            consumer.Subscribe(_topic);

            Console.WriteLine($"Listening for messages ont topic : {_topic} ");


            while (true)
            {

                var consumeResult = consumer.Consume();

                if(consumeResult != null)
                {
                    var customerId = int.Parse(consumeResult.Message.Key);

                    Console.WriteLine($"Id du customer : {customerId} ");

                    _iorderService.DeleteOrdersByCustomerIdAsync(customerId).Wait();


                }



            }




        }




    }
}
