using CustomerService.Kafka.CustomerService.Kafka;
using CustomerService.Models;
using CustomerService.Repositories;

namespace CustomerService.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomerRepository _repository;
        private readonly KafkaProducer _kafkaProducer;

        public CustomersService(ICustomerRepository repository, KafkaProducer kafkaProducer)
        {
            _repository = repository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
            await _repository.GetAllAsync();

        public async Task<Customer?> GetCustomerByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddCustomerAsync(Customer customer) =>
            await _repository.AddAsync(customer);

        public async Task UpdateCustomerAsync(Customer customer) =>
            await _repository.UpdateAsync(customer);

        public async Task DeleteCustomerAsync(int id)
        {

            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new Exception($"Customer with ID {id} does not exist.");
            }

            // Suppression du client
            await _repository.DeleteAsync(id);

            // Envoi du message Kafka après suppression
            await _kafkaProducer.SendMessageAsync("customer-deleted", id.ToString(), $"Customer with ID {id} deleted");
        }
          
    }
}
