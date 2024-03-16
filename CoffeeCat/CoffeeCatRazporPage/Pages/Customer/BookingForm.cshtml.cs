using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer {
    public class BookingFormModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Booking> bookingRepository;

        public BookingFormModel(ICoffeeShopManagerRepository<Booking> bookingRepository) {
            this.bookingRepository = bookingRepository;
        }

        [BindProperty]
        public int AreaId { get; set; }
        [BindProperty]
        public int ShopId { get; set; }
        public async Task<IActionResult> OnGetAsync(int? areaId, int? shopId) {
            Authenticate();
            Authorization();

            AreaId = (int)areaId;
            ShopId = (int)shopId;

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
