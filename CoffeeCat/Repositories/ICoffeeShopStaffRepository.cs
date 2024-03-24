using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICoffeeShopStaffRepository
    {
        Task<List<Booking>> GetBookingsByShopIdAsync(int? shopId);
        Task CreateStaff (User user);
        Task <List<User>> GetUserbyRold(int roleId);
        Task Ban(int id);
        Task Unban(int id);
        bool IsExistedEmail(string email);
        Task<Booking> GetBookingByIdAsync(int? bookingId);
         Task UpdateAsync(Booking entity);
        Task DeleteAsync(Booking entity);
        Task<List<User>> GetCustomersByShopId(int shopId);
        Task<int> GetShopIdByRoleId(int roleId);
        Task<List<User>> GetUsersByRoleIdAndShopId(int roleId, int shopId);
        Task<int?> GetShopIdByOwnerId(int ownerId);
        Task<List<User>> GetStaffByShopId(int shopId);

    }
}
