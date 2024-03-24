using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.Data.SqlClient;
using Repositories;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class UpdateItemModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<MenuItem> itemRepository;
        private readonly ISessionRepository sessionRepository;
        public UpdateItemModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<MenuItem> itemRepository,ISessionRepository sessionRepository)
        {
            this.shopRepository = shopRepository;
            this.itemRepository = itemRepository;
            this.sessionRepository = sessionRepository;
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public MenuItem menuItem { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int shopId)
        {
            Authenticate();
            Authorization();
            var shopOwner = sessionRepository.GetUserByRole(HttpContext.Session.GetInt32("UserId"));
            MenuItems = await itemRepository.GetItemIByShopIdAsync(shopOwner.ShopId);
            menuItem = await itemRepository.GetMenuItemsByIdAsync(id);
            menuItem.ShopId = shopId;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int shopId)
        {
            var existingItem = await itemRepository.GetMenuItemByNameAsync(menuItem.ItemName, shopId);
            if (existingItem != null )
            {
                ErrorMessage = "cat name already exists in this shop.";
                return Page();
            }
            menuItem.ShopId = shopId;
            menuItem.ItemEnabled = true;
            await itemRepository.UpdateAsync(menuItem);

            // Cập nhật lại thông tin phân trang
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