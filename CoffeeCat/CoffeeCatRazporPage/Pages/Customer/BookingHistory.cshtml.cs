using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities; 
using Repositories;
using Microsoft.AspNetCore.Http;


namespace CoffeeCatRazporPage.Pages.Customer
{
    public class BookingHistoryModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Booking> bookingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookingHistoryModel(ICoffeeShopManagerRepository<Booking> bookingRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.bookingRepository = bookingRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<Booking> BookingHistory { get; set; }

        public async Task<IActionResult> OnGet()
        {
            int customerId = 1/*httpContextAccessor.HttpContext.Session.GetInt32("CustomerId") ?? 0*/;

            if (customerId == 0)
            {
                // Redirect to the login page or any other page to get the customer ID
                return RedirectToPage("/LoginPage");
            }

            BookingHistory = await bookingRepository.GetBookingHistoryForCustomerAsync(customerId);

            return Page();
        }
    }
}
