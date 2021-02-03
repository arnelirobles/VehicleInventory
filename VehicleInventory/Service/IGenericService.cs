using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleInventory.Service
{
    public interface IGenericService<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(string id);

        Task<TEntity> CreateAsync(TEntity model);

        Task UpdateAsync(TEntity model);

        Task DeleteAsync(string id);
    }
}