using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleInventory.Models;
using VehicleInventory.Repository;

namespace VehicleInventory.Service
{
    public class VehicleService : ServiceBase, IVehicleService
    {
        public VehicleService(InventoryDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            vehicle.Created = DateTime.Now;
            db.Vehicles.Add(vehicle);
            await db.SaveChangesAsync();

            return vehicle;
        }

        public async Task DeleteAsync(string id)
        {
            var vehicle = await GetByIdAsync(id);
            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAsync()
        {
            return await db.Vehicles.AsNoTracking().ToListAsync();
        }

        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return await db.Vehicles.Where(c => c.VINNumber == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            var vehicleDb = await GetByIdAsync(vehicle.VINNumber);
            vehicleDb.Make = vehicle.Make;
            vehicleDb.Model = vehicle.Model;
            vehicleDb.Year = vehicle.Year;
            vehicleDb.Modified = DateTime.Now;
            db.Entry(vehicleDb).State = EntityState.Modified;

            await db.SaveChangesAsync();
        }
    }
}