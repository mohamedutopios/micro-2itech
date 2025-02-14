using InventaireService.Models;

namespace InventaireService.Repositories
{
    public interface IInventoryRepository
    {
        List<InventoryItem> GetInventory();
        InventoryItem GetStock(int productId);
        void AddStock(InventoryItem item);
        void UpdateStock(InventoryItem item);
        void DeleteStock(int productId);
    }
}
