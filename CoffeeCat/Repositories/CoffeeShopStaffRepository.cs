using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CoffeeShopStaffRepository  : ICoffeeShopStaffRepository
    {
        private CoffeeCatContext context;


        public CoffeeShopStaffRepository(CoffeeCatContext context)
        {
            this.context = context;

        }

        public async Task CreateStaff(User user)
        {
            await context.AddAsync(user);
           await context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetBookingsByShopIdAsync(int? shopId)
        {
            return await context.Bookings
                .Include(b => b.Customer)
                .Where(b => b.Customer.ShopId == shopId)
                .ToListAsync();
        }

        public Task<List<User>> GetUserbyRold(int roleId)
        {
            return context.Users.Where(u => u.RoleId == roleId).ToListAsync();
        }
        public async Task Ban(int id)
        {
            try
            {
                var user = await context.Users.FindAsync(id);

                if (user != null)
                {
                    user.CustomerEnabled = false;

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException($"User with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task Unban(int id)
        {
            try
            {
                var user = await context.Users.FindAsync(id);

                if (user != null)
                {
                    user.CustomerEnabled = true;

                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException($"User with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        public bool IsExistedEmail(string email)
        {
            try
            {
                User? user = context.Users.FirstOrDefault(u => u.CustomerEmail == email);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
