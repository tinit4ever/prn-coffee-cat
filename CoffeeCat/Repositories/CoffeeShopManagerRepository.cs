using Entities;
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
        private  CoffeeCatContext context;

        public CoffeeShopManagerRepository(CoffeeCatContext context)
        {
            this.context = context;
        }

        public async Task<Shop> GetShopByIdAsync(int id)
        {
            return await context.Shops.FindAsync(id);
        }
        public async Task<Table> GetTableByIdAsync(int id)
        {
            return await context.Tables.FindAsync(id);
        }
        public async Task<IQueryable<Shop>> GetAllAsync()
        {
            return await Task.FromResult(context.Shops.AsNoTracking());
        }
        public async Task<IQueryable<Cat>> GetAllCatAsync()
        {
            return await Task.FromResult(context.Cats.AsNoTracking());
        }
        public async Task<Cat> GetCatByIdAsync(int id)
        {
            return await context.Cats.FindAsync(id);
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
        public async Task<List<Table>> GetByShopIdAsync(int shopId)
        {
            return await context.Tables
                .Where(t => t.ShopId == shopId)
                .ToListAsync();
        }
 
    }
}
