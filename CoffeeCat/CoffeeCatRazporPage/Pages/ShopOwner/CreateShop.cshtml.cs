using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class CreateShopModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateShopModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
            Areas = new List<Area>();
        }

        [BindProperty]
        public Shop Shop { get; set; }
        public bool ShopEnabled { get; set; }
        [BindProperty]
        public List<Area> Areas { get; set; }

        public void OnGet() {
            Authenticate();
            Authorization();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            Shop.ShopEnabled = false;
            await shopRepository.AddAsync(Shop);

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
