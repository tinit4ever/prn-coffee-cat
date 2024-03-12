using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
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


    }
}

