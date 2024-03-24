using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;


namespace CoffeeCatRazporPage.Pages.Customer {
    public class BookingHistoryModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Booking> bookingRepository;
        private readonly ICoffeeShopStaffRepository repository;
        private readonly ISessionRepository sessionrepository;

        public BookingHistoryModel(ICoffeeShopManagerRepository<Booking> bookingRepository, ISessionRepository sessionrepository, ICoffeeShopStaffRepository repository) {
            this.bookingRepository = bookingRepository;
            this.sessionrepository = sessionrepository;
            this.repository = repository;
        }
        public List<Booking> BookingHistory { get; set; }
        [BindProperty]
        public int bookingId { get; set; }
        public async Task<IActionResult> OnGet() {
            Authenticate();
            Authorization();
            BookingHistory  = await bookingRepository.GetBookingHistoryForCustomerAsync(HttpContext.Session.GetInt32("UserId"));
            return Page();
        }
        public async Task<IActionResult> OnPostCancelBookingAsync(int bookingId)
        {
            var booking = await repository.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            if (booking.BookingEnabled != false)
            {
                TempData["ErrorMessage"] = "Cannot cancel a booking that is already enabled.";
                return RedirectToPage(); 
            }

            await repository.DeleteAsync(booking);

            return RedirectToPage();
        }

        private void Authenticate()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
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
