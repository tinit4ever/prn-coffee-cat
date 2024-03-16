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
    public class CustomerRepository<T> : ICustomerRepository<T> where T : class
    {
        private readonly CoffeeCatContext context; // Đối tượng DbContext để tương tác với cơ sở dữ liệu

        public CustomerRepository(CoffeeCatContext context)
        {
            this.context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users.Include(u => u.Role).Include(u => u.Shop).FirstOrDefaultAsync(u => u.CustomerEmail == email);
        }
        public async Task AddAsync(Booking entity)
        {
            context.Bookings.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await context.Bookings.FindAsync(id);
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await context.Bookings.ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var booking = await GetByIdAsync(id);
            if (booking == null)
                return false;

            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IQueryable<Shop>> GetShopEnableAsync()
        {
            return await Task.FromResult(context.Shops.Where(s => s.ShopEnabled == true));
        }
        public async Task<IQueryable<Cat>> GetCatEnableAsync()
        {
            return await Task.FromResult(context.Cats.Where(s => s.CatEnabled == true));
        }
        public async Task<IQueryable<Area>> GetAreaEnableAsync()
        {
            return await Task.FromResult(context.Areas.Where(s => s.AreaEnabled == true));
        }
        public async Task<IQueryable<MenuItem>> GetMenuItemEnableAsync()
        {
            return await Task.FromResult(context.MenuItems.Where(s => s.ItemEnabled == true));
        }
        public async Task<IQueryable<Table>> GetTableEnableAsync()
        {
            return await Task.FromResult(context.Tables.Where(s => s.TableEnabled == true));
        }
        public async Task<T> GetShopByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }
    }
}
