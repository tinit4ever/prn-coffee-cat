using Entities;
using Microsoft.CodeAnalysis.Elfie.Serialization;
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
        public async Task<IQueryable<Shop>> GetAllAsync()
        {
            return await Task.FromResult(context.Shops.AsNoTracking());
        }
        public Task<IQueryable<Table>> GetTablesByAreaIdAsync(int areaId)
        {
            return Task.FromResult(context.Tables.Where(c => c.AreaId == areaId));
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
        public async Task ToggleEnabledAsync(int id, bool isEnabled)
        {
            var shop = await context.Shops.FindAsync(id);
            if (shop != null)
            {
                shop.ShopEnabled = isEnabled;
                await context.SaveChangesAsync();
            }
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
    }
}
