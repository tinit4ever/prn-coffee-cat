using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;
namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class ShopManager : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> repository;
        private readonly ISessionRepository sessionrepository;
        public ShopManager(ICoffeeShopManagerRepository<Shop> repository , ISessionRepository sessionrepository) {
            this.repository = repository;
            this.sessionrepository = sessionrepository;
        }

        [BindProperty(SupportsGet = true)]

        public Shop Shop { get; set; }


        public async Task OnGetAsync() {
            Authenticate();
            Authorization();
            var shopOwner =  sessionrepository.GetUserById(2);
            Shop = await repository.GetShopByIdAsync(shopOwner.ShopId);
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
