using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class CreateItemModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<MenuItem> itemRepository;
        private readonly ISessionRepository sessionrepository;
        public CreateItemModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<MenuItem> itemRepository, ISessionRepository sessionrepository)
        {
            this.shopRepository = shopRepository;
            this.itemRepository = itemRepository;
            this.sessionrepository = sessionrepository;
            MenuItems = new List<MenuItem>();
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public MenuItem menuItem { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Authenticate();
            Authorization();
            // L?y danh sách mèo theo areaId
            var shopOwner = sessionrepository.GetUserByRole(HttpContext.Session.GetInt32("UserId"));
            MenuItems = await itemRepository.GetItemIByShopIdAsync(shopOwner.ShopId);

            menuItem = await itemRepository.GetMenuItemsByIdAsync(shopOwner.ShopId);
 

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
         
            var existingMenuItem = await itemRepository.GetMenuItemByNameAsync(menuItem.ItemName, shop.ShopId);
            if (existingMenuItem != null)
            {
                ErrorMessage = "Item name already exists in this shop.";
                return Page();
            }
            
            if (string.IsNullOrWhiteSpace(menuItem.ItemName))
            {
                ErrorMessage = "Item name cannot be empty.";
                return Page();
            }
            menuItem.ItemEnabled = true;
            var shopOwner = sessionrepository.GetUserByRole( HttpContext.Session.GetInt32("UserId"));
            menuItem.ShopId = shopOwner.ShopId;
            await itemRepository.AddAsync(menuItem);


            return RedirectToPage("./MenuItemManager", new { shopId = menuItem.ShopId, pageIndex = 1 });
        }
        private void Authenticate()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                HttpContext.Response.Redirect("/Auth/SignIn");
            }
        }

        private void Authorization()
        {
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId.HasValue)
            {
                if (roleId.Value != 2)
                {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}
