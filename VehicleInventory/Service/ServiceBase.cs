using System;
using VehicleInventory.Repository;

namespace VehicleInventory.Service
{
    public abstract class ServiceBase
    {
        private InventoryDBContext _db;
        internal InventoryDBContext db { get { return _db; } }

        /// <summary>
        /// Base service constructor requires database context
        /// </summary>
        /// <param name="dbContext"></param>
        public ServiceBase(InventoryDBContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext", "database context is required");

            _db = dbContext;
        }
    }
}