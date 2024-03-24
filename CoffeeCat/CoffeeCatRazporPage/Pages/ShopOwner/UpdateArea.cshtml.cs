using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class UpdateAreaModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;
        private readonly ISessionRepository sessionrepository;
        public UpdateAreaModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository, ISessionRepository sessionRepository) {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
            this.sessionrepository = sessionRepository;
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public Area area { get; set; }
        public List<Area> Areas { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int shopId) {
            Authenticate();
            Authorization();
            var shopOwner = sessionrepository.GetUserByRole(HttpContext.Session.GetInt32("UserId"));
            Areas = await areaRepository.GetAreaByShopIdAsync(shopOwner.ShopId);
            area = await areaRepository.GetAreaByIdAsync(id);
            area.ShopId = shopId;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int shopId) {
            var existingArea = await areaRepository.GetAreaByNameAsync(area.AreaName, shopId);
            if (existingArea != null)
            {
                ErrorMessage = "cat name already exists in this area.";
                return Page();
            }
            area.ShopId = shopId;
            area.AreaEnabled = true;
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