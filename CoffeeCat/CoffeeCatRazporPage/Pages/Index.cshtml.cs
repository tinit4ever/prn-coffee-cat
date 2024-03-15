using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeeCatRazporPage.Pages {
    public class IndexModel : PageModel {
        public void OnGet() {
            Dispatcher();
        }
        private void Dispatcher() {
            int? userId = HttpContext.Session.GetInt32("UserId");
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            string pageIndex = "/Customer/CustomerHomePage";

            if (userId != null) {
                if (roleId.HasValue) {
                    pageIndex = roleDivition(userId.Value);
                    HttpContext.Response.Redirect(pageIndex);
                }
            } else {
                HttpContext.Response.Redirect(pageIndex);
            }
        }

        private String roleDivition(int role) {
            if (role == 1) {
                return "/Admin/Admin";
            } else if (role == 2) {
                return "/ShopOwner/ShopManager";
            } else if (role == 3) {
                return "/Staff/StaffHomePage";
            } else {
                return "/Customer/CustomerHomePage";
            }
        }
    }
}
