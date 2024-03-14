using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Home {
    public class UserProfileModel : PageModel {
        private readonly ISessionRepository _sessionRepository;

        public UserProfileModel(ISessionRepository sessionRepository) {
            _sessionRepository = sessionRepository;
        }
        public void OnGet() {
        }

        public IActionResult OnPostLogout() {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("RoleId");
            return RedirectToPage("/Index");
        }
    }
}
