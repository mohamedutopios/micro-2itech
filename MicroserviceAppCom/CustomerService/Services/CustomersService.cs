using CustomerService.Models;
using CustomerService.Repositories;

namespace CustomerService.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomerRepository _repository;

        public CustomersService(ICustomerRepository customerRepository)
        {
            _repository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync() =>
            await _repository.GetAllAsync();

        public async Task<Customer?> GetCustomerByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddCustomerAsync(Customer customer) =>
            await _repository.AddAsync(customer);

        public async Task UpdateCustomerAsync(Customer customer) =>
            await _repository.UpdateAsync(customer);

        public async Task DeleteCustomerAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
