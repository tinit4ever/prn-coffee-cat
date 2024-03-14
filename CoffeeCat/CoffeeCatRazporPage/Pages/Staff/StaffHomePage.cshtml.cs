using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCatRazporPage.Pages.Staff {
    public class StaffHomePageModel : PageModel {
        public void OnGet() {
            Authenticate();
            Authorization();
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
