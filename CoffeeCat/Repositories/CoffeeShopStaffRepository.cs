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

        public async Task CreateStaff(User user)
        {
            await context.AddAsync(user);
           await context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetBookingsByShopIdAsync(int? shopId)
        {
            var bookingsForShop = await context.Bookings
                .Include(b => b.Customer)
                .Include(t => t.Tables)
                .Include(t => t.Items)
                .Where(b => context.Tables
                    .Include(t => t.Area)
                    .Any(t => t.Area.ShopId == shopId))
                .ToListAsync();

            return bookingsForShop;
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

        /*private int GetTableId(int? shopId)
        {
            var area = context.Areas.Find(shopId);
            var table = context.Tables.Find(area.AreaId);

            int tableId = table.TableId;

            return tableId;
        }*/
        public async Task<List<User>> GetCustomersByShopId(int shopId)
        {
            return await context.Users.Where(u => u.RoleId == 4 && u.ShopId == shopId).ToListAsync();
        }
        public async Task<int> GetShopIdByRoleId(int roleId)
        {
            var userWithRoleId2 = await context.Users.FirstOrDefaultAsync(u => u.RoleId == roleId);
            if (userWithRoleId2 != null)
            {
                return userWithRoleId2.ShopId ?? 0;
            }
            return 0; // Trả về 0 nếu không tìm thấy user nào có roleId = 2
        }
        public async Task<List<User>> GetUsersByRoleIdAndShopId(int roleId, int shopId)
        {
            return await context.Users
                .Where(u => u.RoleId == roleId && u.ShopId == shopId)
                .ToListAsync();
        }
        public async Task<int?> GetShopIdByOwnerId(int ownerId)
        {
            var owner = await context.Users.FirstOrDefaultAsync(u => u.CustomerId == ownerId && u.RoleId == 2);
            return owner?.ShopId;
        }
        public async Task<List<User>> GetStaffByShopId(int shopId)
        {
            return await context.Users
                .Where(u => u.RoleId == 3 && u.ShopId == shopId)
                .ToListAsync();
        }
    }
    }

