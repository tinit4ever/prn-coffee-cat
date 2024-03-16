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
        Task<Booking> GetBookingByIdAsync(int? bookingId);
         Task UpdateAsync(Booking entity);
    }
}
