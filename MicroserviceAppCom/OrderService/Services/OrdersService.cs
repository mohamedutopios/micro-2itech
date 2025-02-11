using OrderService.Clients;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _repository;
        private readonly ICustomerServiceClient _customerServiceClient;
        private readonly IProductServiceClient _productServiceClient;

        public OrdersService(IOrderRepository repository, ICustomerServiceClient customerServiceClient, IProductServiceClient productServiceClient)
        {
            _repository = repository;
            _customerServiceClient = customerServiceClient;
            _productServiceClient = productServiceClient;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
            await _repository.GetAllAsync();

        public async Task<Order?> GetOrderByIdAsync(string id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Order> AddOrderAsync(Order order)
        {
            var customerExist = await _customerServiceClient.CustomerExistsAsync(order.CustomerId);
            var productExist = await _productServiceClient.ProductExistAsync(order.ProductId);

            if(!customerExist || !productExist)
            {
                return null;
            }

            await _repository.AddAsync(order);

            return order;

        }

        public async Task UpdateOrderAsync(Order order) =>
            await _repository.UpdateAsync(order);

        public async Task DeleteOrderAsync(string id) =>
            await _repository.DeleteAsync(id);
    }
}
