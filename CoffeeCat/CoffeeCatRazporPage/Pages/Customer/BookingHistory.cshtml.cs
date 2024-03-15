using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;


namespace CoffeeCatRazporPage.Pages.Customer {
    public class BookingHistoryModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Booking> bookingRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BookingHistoryModel(ICoffeeShopManagerRepository<Booking> bookingRepository, IHttpContextAccessor httpContextAccessor) {
            this.bookingRepository = bookingRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        public List<Booking> BookingHistory { get; set; }

        public async Task<IActionResult> OnGet() {
       /*     Authenticate();
            Authorization();*/
            int customerId = 1/*httpContextAccessor.HttpContext.Session.GetInt32("CustomerId") ?? 0*/;

            if (customerId == 0) {
                // Redirect to the login page or any other page to get the customer ID
                return RedirectToPage("/LoginPage");
            }

            BookingHistory = await bookingRepository.GetBookingHistoryForCustomerAsync(customerId);

            return Page();
        }

        private void Authenticate() {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) {
                HttpContext.Response.Redirect("/Auth/SignIn");
            }
        }

        private void Authorization() {
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId.HasValue) {
                if (roleId.Value != 4) {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}
