using OrderService.DTO;

namespace OrderService.Clients
{
    public interface ICustomerServiceClient
    {
        Task<bool> CustomerExistsAsync(int customerId);
        Task<CustomerDTO?> GetCustomerDetailsAsync(int customerId);
    }
}
