using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class CreateAreaModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateAreaModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
            Areas = new List<Area>();
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public Area area { get; set; }
        public List<Area> Areas { get; set; }

        public async Task<IActionResult> OnGetAsync(int ShopId) {
            Authenticate();
            Authorization();
            // Lấy danh sách mèo theo areaId
            Areas = await areaRepository.GetAreaByShopIdAsync(ShopId);

            area = await areaRepository.GetAreaByIdAsync(ShopId);
            if (area == null) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            /*    if (!ModelState.IsValid)
                {
                    return Page();
                }*/
            area.AreaEnabled = true;

            await areaRepository.AddAsync(area);


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
