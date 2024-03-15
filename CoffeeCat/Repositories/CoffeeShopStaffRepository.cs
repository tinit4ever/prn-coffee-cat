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
        public async Task<List<Booking>> GetBookingsByShopIdAsync(int? shopId)
        {
            return await context.Bookings
                .Include(b => b.Customer)
                .Where(b => b.Customer.ShopId == shopId)
                .ToListAsync();
        }
    }
}
