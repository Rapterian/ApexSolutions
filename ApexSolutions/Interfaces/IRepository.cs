using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApexCare.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);                  // Add a new entity
        Task<IEnumerable<T>> GetAllAsync();          // Get all entities
        Task<T> GetByIdAsync(int id);                // Get an entity by ID
        Task<T> UpdateAsync(T entity);                // Update an existing entity
        Task<bool> DeleteAsync(int id);               // Delete an entity by ID
    }
}
