using Entities;

namespace Repositories
{
    public interface ICoffeeShopManagerRepository<T> where T : class
    {
        Task<Shop> GetShopByIdAsync(int id);
        Task<Cat> GetCatByIdAsync(int id);
        Task<IQueryable<Shop>> GetAllAsync();
        Task<IQueryable<Cat>> GetAllCatAsync();
        /*Task<IEnumerable<Shop>> GetEnabledShopsAsync();*/
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task ToggleEnabledAsync(int id, bool isEnabled);
        Task<List<Table>> GetByAreaIdAsync(int AreaId);
        Task<Table> GetTableByIdAsync(int id);
    }
}