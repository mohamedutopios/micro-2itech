using OrderService.DTO;
using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(string id);
        Task<OrderDTO?> CreateOrderAsync(OrderDTO orderDto);
        Task<bool> UpdateOrderAsync(string id, OrderDTO orderDto);
        Task<bool> DeleteOrderAsync(string id);
       
    }
}
