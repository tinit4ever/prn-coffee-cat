using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Admin;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Admin {
    [BindProperties]
    public class CreateModel : PageModel {
        private readonly IAdminRepository _adminRepository;
        private readonly ISessionRepository _sessionRepository;

        public User User { get; set; }
        public Shop Shop { get; set; }

        public CreateModel(IAdminRepository adminRepository, ISessionRepository sessionRepository) {
            _adminRepository = adminRepository;
            _sessionRepository = sessionRepository;
            User = new User();
            Shop = new Shop();
        }
        public void OnGet() {
            Authenticate();
            Authorization();
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }
            User.Shop = Shop;
            User.RoleId = 2;
            await _adminRepository.CreateShopOwner(User);
            return RedirectToPage("/Admin/Admin");
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
                if (roleId.Value != 1) {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}
