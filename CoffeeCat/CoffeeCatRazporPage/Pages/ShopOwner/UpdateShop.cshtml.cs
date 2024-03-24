using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class UpdateShopModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateShopModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Shop Shop { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int id) {
            Authenticate();
            Authorization();
            Shop = await shopRepository.GetShopByIdAsync(id);

            if (Shop == null) {
                return NotFound();
            }

            // Kh?i t?o ShopName n?u nó là null
            if (Shop.ShopName == null) {
                Shop.ShopName = "";
            }


            return null;
        }

        public async Task<IActionResult> OnPostAsync() {
            
            Shop.ShopEnabled = true;
            await shopRepository.UpdateAsync(Shop);



            return RedirectToPage("./ShopManager", new { pageIndex = 1 });
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
