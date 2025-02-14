using OrderService.DTO;

namespace OrderService.Clients
{
    public interface IInventoryServiceClient
    {
        Task<InventoryItemDTO> GetStock(int productId);
    }
}
