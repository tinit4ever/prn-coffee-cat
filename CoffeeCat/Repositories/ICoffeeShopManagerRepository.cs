using Entities;

namespace Repositories
{
    public interface ICoffeeShopManagerRepository<T> where T : class
    {
        Task<Shop> GetShopByIdAsync(int id);
        Task<Area> GetAreaByIdAsync(int id);
        Task<Table> GetTableByIdAsync(int id);

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

        Task<IQueryable<Table>> GetTablesByAreaIdAsync(int areaId);
        Task<List<Table>> GetTableByAreaIdAsync(int AreaId);
        Task<Shop> GetShopByIdAsync(int? shopId);
        Task<List<Table>> GetAllTableAsync();
        Task<List<MenuItem>> GetAllMenuItemAsync();
        Task AddMenuItemsToBookingAsync(int bookingId, List<int> menuItems);
        Task AddAsync(Booking entity);
        Task AddMultipleAsync(List<Booking> entities);
        Task AddTablesToBookingAsync(int bookingId, List<int> tableIds);
        Task<Booking> GetBookingByTableAndTimeAsync(int tableId, DateTime bookingStartTime, DateTime bookingEndTime);
        Task<List<Table>> GetAvailableTablesAsync(int areaId, DateTime startTime, DateTime endTime);
        Task<List<Booking>> GetBookingHistoryForCustomerAsync(int customerId);
        Task<List<MenuItem>> GetAllMenuItemByShopIdAsync(int ShopId);
    }
}