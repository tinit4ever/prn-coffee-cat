using Entities;

namespace Repositories
{
    public interface ICoffeeShopManagerRepository<T> where T : class
    {
        Task<Shop> GetShopByIdAsync(int id);
        Task<Area> GetAreaByIdAsync(int id);
        Task<Cat> GetCatByIdAsync(int id);
        Task<IQueryable<Shop>> GetAllAsync();
        Task<List<Area>> GetAreaByShopIdAsync(int shopId);

        Task<IQueryable<Cat>> GetCatsByAreaIdAsync(int areaId);
        Task<IQueryable<Area>> GetAreasByShopIdAsync(int shopId);
        /*Task<IEnumerable<Shop>> GetEnabledShopsAsync();*/
        Task<List<Cat>> GetCatByAreaIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task ToggleEnabledAsync(int id, bool isEnabled);

        Task<Table> GetTableByIdAsync(int id);
    }
}