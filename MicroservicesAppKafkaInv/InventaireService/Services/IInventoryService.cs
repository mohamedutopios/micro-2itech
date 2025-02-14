using InventaireService.Models;

namespace InventaireService.Services
{
    public interface IInventoryService
    {
        List<InventoryItem> GetInventory();
        InventoryItem GetStock(int productId);
        void AddStock(InventoryItem item);
        void UpdateStock(InventoryItem item);
        void DeleteStock(int productId);
    }
}
