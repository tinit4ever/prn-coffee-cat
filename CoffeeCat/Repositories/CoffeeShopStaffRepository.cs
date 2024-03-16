using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CoffeeShopStaffRepository : ICoffeeShopStaffRepository
    {
        private CoffeeCatContext context;


        public CoffeeShopStaffRepository(CoffeeCatContext context)
        {
            this.context = context;

        }
        public async Task<List<Booking>> GetBookingsByShopIdAsync(int? shopId)
        {
            return await context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Tables)
                .Include(b => b.Items)
                .Where(b => b.Customer.ShopId == shopId)
                .ToListAsync();
        }
        public async Task<Booking> GetBookingByIdAsync(int? bookingId)
        {
            if (bookingId == null)
            {
                return null;
            }

            return await context.Bookings.FindAsync(bookingId);
        }
        public async Task UpdateAsync(Booking entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Booking entity)
        {
            
            foreach (var menuItem in context.MenuItems.Include(m => m.Bookings).Where(m => m.Bookings.Any(b => b.BookingId == entity.BookingId)))
            {
                menuItem.Bookings.Remove(entity);
            }

            foreach (var table in context.Tables.Include(t => t.Bookings).Where(t => t.Bookings.Any(b => b.BookingId == entity.BookingId)))
            {
                table.Bookings.Remove(entity);
            }

            // Tiếp tục xóa Booking
            context.Bookings.Remove(entity);

            await context.SaveChangesAsync();
        }
    }
    }
