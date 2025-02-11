using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService.Configurations;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<IEnumerable<Order>> GetAllAsync() =>
            await _orders.Find(order => true).ToListAsync();

        public async Task<Order?> GetByIdAsync(string id) =>
            await _orders.Find(order => order.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Order order) =>
            await _orders.InsertOneAsync(order);

        public async Task UpdateAsync(Order order) =>
            await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _orders.DeleteOneAsync(o => o.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task DeleteByCustomerIdAsync(int customerId) =>
            await _orders.DeleteManyAsync(o => o.CustomerId == customerId);

        public async Task DeleteByProductIdAsync(int productId) =>
            await _orders.DeleteManyAsync(o => o.ProductId == productId);
    }
}
