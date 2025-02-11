using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _repository;

        public OrdersService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
            await _repository.GetAllAsync();

        public async Task<Order?> GetOrderByIdAsync(string id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddOrderAsync(Order order) =>
            await _repository.AddAsync(order);

        public async Task UpdateOrderAsync(Order order) =>
            await _repository.UpdateAsync(order);

        public async Task DeleteOrderAsync(string id) =>
            await _repository.DeleteAsync(id);
    }
}
