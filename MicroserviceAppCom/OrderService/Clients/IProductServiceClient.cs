using OrderService.DTO;

namespace OrderService.Clients
{
    public interface IProductServiceClient
    {
        Task<bool> ProductExistsAsync(int productId);
        Task<ProductDTO?> GetProductDetailsAsync(int productId);
    }
}
