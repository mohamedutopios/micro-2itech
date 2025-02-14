using InventaireService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventaireService.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        public DbSet<InventoryItem> Inventory { get; set; }
    }
}
