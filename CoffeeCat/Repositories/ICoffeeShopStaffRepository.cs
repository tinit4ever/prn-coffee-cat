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
    }
}
