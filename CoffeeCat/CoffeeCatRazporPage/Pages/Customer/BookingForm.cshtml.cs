using System;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer
{
    public class BookingFormModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Booking> bookingRepository;

        public BookingFormModel(ICoffeeShopManagerRepository<Booking> bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        [BindProperty]
        public int AreaId { get; set; }
        [BindProperty]
        public int ShopId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? areaId, int? shopId) 
        {
          

            AreaId = /*(int)areaId*/ 1;
            ShopId = /*(int)shopId*/ 1;

            return Page();
        }

        
    }
}
