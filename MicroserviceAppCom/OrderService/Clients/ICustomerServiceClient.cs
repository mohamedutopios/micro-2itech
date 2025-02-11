namespace OrderService.Clients
{
    public interface ICustomerServiceClient
    {

        Task<bool> CustomerExistsAsync(int customerId);

    }
}
