using Entities;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CoffeeShopManagerRepository<T> : ICoffeeShopManagerRepository<T> where T : class
    {
        private CoffeeCatContext context;
        private readonly DbSet<T> _dbSet;

        public CoffeeShopManagerRepository(CoffeeCatContext context)
        {
            this.context = context;

        }

        public async Task<Shop> GetShopByIdAsync(int id)
        {
            return await context.Shops.FindAsync(id);
        }
 
        public Task<IQueryable<Cat>> GetCatsByAreaIdAsync(int areaId)
        {
            return Task.FromResult(context.Cats.Where(c => c.AreaId == areaId));
        }
        public async Task<IQueryable<Shop>> GetShopAsync(int shopId)
        {
            return context.Shops.Where(c => c.ShopId == shopId);
        }

        public Task<IQueryable<Table>> GetTablesByAreaIdAsync(int areaId)
        {
            return Task.FromResult(context.Tables.Where(c => c.AreaId == areaId));
        }
        public Task<IQueryable<MenuItem>> GetMenuItemsByShopIdAsync(int shopId)
        {
            return Task.FromResult(context.MenuItems.Where(i => i.ShopId == shopId));
        }
        public Task<IQueryable<Area>> GetAreasByShopIdAsync(int shopId)
        {
            return Task.FromResult(context.Areas.Where(c => c.ShopId == shopId));
        }
        
        public async Task<List<Cat>> GetCatByAreaIdAsync(int AreaId)
        {
            return await context.Cats
                .Where(t => t.AreaId == AreaId)
                .ToListAsync();
        }
        public async Task<List<Table>> GetTableByAreaIdAsync(int AreaId)
        {
            return await context.Tables
                .Where(t => t.AreaId == AreaId)
                .ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddTablesToBookingAsync(int bookingId, List<int> tableIds)
        {
            // Lấy đặt bàn từ cơ sở dữ liệu
            var booking = await context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                // Xử lý khi không tìm thấy đặt bàn
                return;
            }

            // Lấy danh sách các bàn từ cơ sở dữ liệu
            var tablesToAdd = await context.Tables.Where(t => tableIds.Contains(t.TableId)).ToListAsync();

            // Thêm danh sách các bàn vào đặt bàn
            foreach (var tableToAdd in tablesToAdd)
            {
                booking.Tables.Add(tableToAdd);
            }


            await context.SaveChangesAsync();
        }
        public async Task AddMenuItemsToBookingAsync(int bookingId, List<int> menuItems)
        {           
            var booking = await context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
               
                return;
            }
                   
            var menuItemsToAdd = await context.MenuItems.Where(t => menuItems.Contains(t.ItemId)).ToListAsync();

           
            foreach (var menuItemToAdd in menuItemsToAdd)
            {
                booking.Items.Add(menuItemToAdd);
            }


            await context.SaveChangesAsync();
        }
        public async Task<List<MenuItem>> GetAllMenuItemAsync()
         {
            return await context.MenuItems.ToListAsync();
    }
        public async Task<List<MenuItem>> GetAllMenuItemByShopIdAsync(int ShopId)
        {
            return await context.MenuItems.Where(i => i.ShopId == ShopId && i.ItemEnabled == true)
                .ToListAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }



        /*      public async Task<IEnumerable<Shop>> GetEnabledShopsAsync()
              {
                  return await context.Shops.Where(s => s.ShopEnabled).ToListAsync();
              }
      */
        public async Task<List<Table>> GetAllTableAsync()
        {
            return await context.Tables.ToListAsync();
        }
        public async Task ToggleEnabledAsync(int id, bool isEnabled)
        {
            var shop = await context.Shops.FindAsync(id);
            if (shop != null)
            {
                shop.ShopEnabled = isEnabled;
                await context.SaveChangesAsync();
            }
        }
        public async Task<List<MenuItem>> GetItemIByShopIdAsync(int ShopId)
        {
            return await context.MenuItems
               .Where(t => t.ShopId == ShopId)
               .ToListAsync();
        }
        public async Task<List<Area>> GetAreaByShopIdAsync(int ShopId)
        {
            return await context.Areas
                .Where(t => t.ShopId == ShopId)
                .ToListAsync();
        }
        public async Task<Area> GetAreaByIdAsync(int id)
        {
            return await context.Areas.FindAsync(id);
        }
        public async Task<MenuItem> GetMenuItemsByIdAsync(int id)
        {
            return await context.MenuItems.FindAsync(id);
        }
        public async Task<Cat> GetCatByIdAsync(int id)
        {
            return await context.Cats.FindAsync(id);
        }
        public async Task<Table> GetTableByIdAsync(int id)
        {

            return await context.Tables.FindAsync(id);

        }
        public async Task<Shop> GetShopByIdAsync(int? shopId)
        {
            if (shopId == null)
            {
                return null;
            }

            return await context.Shops.FindAsync(shopId);
        }
        public async Task AddAsync(Booking entity)
        {
            context.Bookings.Add(entity);
            await context.SaveChangesAsync();
        }
        public async Task<Booking> GetBookingByTableAndTimeAsync(int tableId, DateTime bookingStartTime, DateTime bookingEndTime)
        {
            var booking = await context.Bookings
                .Include(b => b.Tables)
                .FirstOrDefaultAsync(b => b.Tables.Any(t => t.TableId == tableId && t.TableEnabled == true) &&
                                           b.BookingStartTime <= bookingEndTime &&
                                           b.BookingEndTime >= bookingStartTime);

            return booking;
        }
 
        public async Task<List<Table>> GetAvailableTablesAsync(int areaId, DateTime startTime, DateTime endTime)
        {
            var unavailableTables = await context.Tables
                .Where(table => table.AreaId == areaId && table.Bookings.Any(booking =>
                    (booking.BookingStartTime < endTime && booking.BookingEndTime > startTime)))
                .ToListAsync();

            var allTablesInArea = await context.Tables
                .Where(table => table.AreaId == areaId)
                .ToListAsync();

            var availableTables = allTablesInArea.Except(unavailableTables).ToList();

            return availableTables;
        }

        public async Task<List<Booking>> GetBookingHistoryForCustomerAsync(int customerId)
        {
            return await context.Bookings.Include(b => b.Tables )
            .Include(b => b.Items).Where(b => b.CustomerId == customerId).ToListAsync();
        }

   

        public async Task AddMultipleAsync(List<Booking> entities)
        {
            context.Bookings.AddRange(entities);
            await context.SaveChangesAsync();
        }


    }
}

