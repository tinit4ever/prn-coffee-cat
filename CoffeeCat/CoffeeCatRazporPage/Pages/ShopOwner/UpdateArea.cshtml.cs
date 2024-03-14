using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class UpdateAreaModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateAreaModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public Area area { get; set; }
        public List<Area> Areas { get; set; }


        public async Task<IActionResult> OnGetAsync(int id, int shopId) {
            Authenticate();
            Authorization();
            Areas = await areaRepository.GetAreaByShopIdAsync(shopId);
            area = await areaRepository.GetAreaByIdAsync(id);
            area.ShopId = shopId;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int shopId) {
            area.ShopId = shopId;
            area.AreaEnabled = false;
            await areaRepository.UpdateAsync(area);

            // Cập nhật lại thông tin phân trang
            return RedirectToPage("./AreaManager", new { shopId = area.ShopId, pageIndex = 1 });
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