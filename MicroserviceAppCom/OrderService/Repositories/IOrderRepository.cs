using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(string id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task<bool> DeleteAsync(string id);
        Task DeleteByCustomerIdAsync(int customerId);
        Task DeleteByProductIdAsync(int productId);
    }
}
