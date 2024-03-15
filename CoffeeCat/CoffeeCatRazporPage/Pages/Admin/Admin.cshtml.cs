using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Admin;
using Repositories.Auth;
namespace CoffeeCatRazporPage.Pages.Admin {
    public class AdminModel : PageModel {
        private readonly IAdminRepository _adminRepository;
        private readonly ISessionRepository _sessionRepository;
        public IEnumerable<User> Users { get; set; }
        public User? LoggedInUser { get; set; }

        public AdminModel(IAdminRepository adminRepository, ISessionRepository sessionRepository) {
            _adminRepository = adminRepository;
            _sessionRepository = sessionRepository;
        }
        public void OnGet() {
            Authenticate();
            Authorization();
            SetUserSession();
            if (LoggedInUser != null) {
                TempData["loggedIn"] = "Hello " + LoggedInUser.CustomerName;
            }
            SetUsers();
        }

        public async Task<IActionResult> OnPostBanAsync(int customerId) {
            await _adminRepository.Ban(customerId);
            return RedirectToPage("/Admin/Admin");
        }

        public async Task<IActionResult> OnPostUnbanAsync(int customerId) {
            await _adminRepository.Unban(customerId);
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
        private void SetUserSession() {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue) {
                LoggedInUser = _sessionRepository.GetUserById(userId.Value);
            }
        }

        private void SetUsers() {
            Users = _adminRepository.GetAllAccount();
        }
    }
}
