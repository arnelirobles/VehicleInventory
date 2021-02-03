using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleInventory.Models
{
    public class Vehicle
    {
        [Key]
        public string VINNumber { get; set; }

        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Modified { get; set; }
    }
}