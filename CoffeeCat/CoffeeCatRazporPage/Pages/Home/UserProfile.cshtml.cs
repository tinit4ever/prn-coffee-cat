using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Home {
    public class UserProfileModel : PageModel {
        private readonly ISessionRepository _sessionRepository;

        public User User { get; set; }

        public UserProfileModel(ISessionRepository sessionRepository) {
            _sessionRepository = sessionRepository;
        }
        public void OnGet() {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue) {
                User = _sessionRepository.GetUserById(userId.Value);
            }
        }

        public IActionResult OnPostLogout() {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("RoleId");
            return RedirectToPage("/Index");
        }
    }
}
