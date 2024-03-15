using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class ShopManager : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> repository;

        public ShopManager(ICoffeeShopManagerRepository<Shop> repository) {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]

        public Shop Shop { get; set; }


        public async Task OnGetAsync(int shopId) {
            // Lấy danh sách cửa hàng từ repository
            Shop = await repository.GetShopByIdAsync(shopId);
        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled) {
            var shop = await repository.GetShopByIdAsync(id);

            if (shop == null) {
                return NotFound();
            }

            shop.ShopEnabled = isEnabled;
            await repository.UpdateAsync(shop);

            return RedirectToPage();
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
                if (roleId.Value != 2) {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}
