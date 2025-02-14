using InventaireService.Data;
using InventaireService.Models;

namespace InventaireService.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;
        public InventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public List<InventoryItem> GetInventory() => _context.Inventory.ToList();
        public InventoryItem GetStock(int productId) => _context.Inventory.FirstOrDefault(i => i.ProductId == productId);
        public void AddStock(InventoryItem item)
        {
            _context.Inventory.Add(item);
            _context.SaveChanges();
        }
        public void UpdateStock(InventoryItem item)
        {
            var existingItem = _context.Inventory.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity = item.Quantity;
                _context.SaveChanges();
            }
        }
        public void DeleteStock(int productId)
        {
            var item = _context.Inventory.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _context.Inventory.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
