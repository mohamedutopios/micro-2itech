namespace OrderService.Clients
{
    public interface IProductServiceClient
    {
        Task<bool> ProductExistAsync(int productId);


    }
}
