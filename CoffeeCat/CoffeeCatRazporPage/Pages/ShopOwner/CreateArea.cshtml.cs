using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class CreateAreaModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;
        private readonly ISessionRepository sessionrepository;
        public CreateAreaModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository, ISessionRepository sessionrepository) {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
            this.sessionrepository = sessionrepository;
            Areas = new List<Area>();
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public Area area { get; set; }
        public List<Area> Areas { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync() {
            Authenticate();
            Authorization();
            var shopOwner = sessionrepository.GetUserByRole( HttpContext.Session.GetInt32("UserId"));
            Areas = await areaRepository.GetAreaByShopIdAsync(shopOwner.ShopId);

            area = await areaRepository.GetAreaByIdAsync(shopOwner.ShopId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(area.AreaName))
            {
                ErrorMessage = "Area name cannot be empty.";
                return Page();
            }

            var shopOwner = sessionrepository.GetUserByRole( HttpContext.Session.GetInt32("UserId"));
            var existingArea = await areaRepository.GetAreaByNameAsync(area.AreaName, shopOwner.ShopId);
            if (existingArea != null)
            {
                ErrorMessage = "Area name already exists in this shop.";
                return Page();
            }

            area.AreaEnabled = true;
            area.ShopId = shopOwner.ShopId;
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
