using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Staff {
    public class StaffHomePageModel : PageModel {
        private readonly ICoffeeShopStaffRepository _cofffeeShopStaffRepository;
        private readonly ISessionRepository sessionrepository;
        public StaffHomePageModel(ICoffeeShopStaffRepository cofffeeShopStaffRepository ,ISessionRepository sessionrepository) 
        {
            _cofffeeShopStaffRepository = cofffeeShopStaffRepository;
            this.sessionrepository = sessionrepository;
        }
        [BindProperty(SupportsGet = true)]
        public List<Booking> Bookings { get; set; }
        public async Task OnGet() {
            Authenticate();
            Authorization();
            var staff =  sessionrepository.GetUserByRole(3);
            Bookings = await _cofffeeShopStaffRepository.GetBookingsByShopIdAsync(staff.ShopId);
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
                if (roleId.Value != 3) {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}
