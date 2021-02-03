using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VehicleInventory.Models;

namespace VehicleInventory.Repository
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new InventoryDBContext(serviceProvider.GetRequiredService<DbContextOptions<InventoryDBContext>>()))
            {
                if (context.Vehicles.Any())
                    return;

                context.Vehicles.AddRange(
                    new Vehicle
                    {
                        VINNumber = "DAC2169",
                        Make = "Toyota",
                        Model = "Vios",
                        Year = 2018,
                        Created = DateTime.Now
                    },
                    new Vehicle
                    {
                        VINNumber = "DAC2160",
                        Make = "Toyota",
                        Model = "Vios",
                        Year = 2017,
                        Created = DateTime.Now
                    }

                    );
                context.SaveChanges();
            }
        }
    }
}