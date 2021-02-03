using Microsoft.EntityFrameworkCore;
using VehicleInventory.Models;

namespace VehicleInventory.Repository
{
    public class InventoryDBContext : DbContext
    {
        public InventoryDBContext(DbContextOptions<InventoryDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }
    }
}