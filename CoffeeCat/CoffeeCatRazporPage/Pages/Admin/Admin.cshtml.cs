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
        public async Task OnGet() {
            int loggedUserId = HttpContext.Session.GetInt32("UserId") ?? 1;

            LoggedInUser = await _sessionRepository.getUserByIdAsync(loggedUserId);

            if (LoggedInUser != null) {
                TempData["loggedIn"] = "Hello " + LoggedInUser.CustomerName!;
            }

            Users = _adminRepository.GetAllAccount();
        }

        public async Task<IActionResult> OnPostBanAsync(int customerId) {
            await _adminRepository.Ban(customerId);
            return RedirectToPage("/Admin/Admin");
        }

        public async Task<IActionResult> OnPostUnbanAsync(int customerId) {
            await _adminRepository.Unban(customerId);
            return RedirectToPage("/Admin/Admin");
        }
    }
}
