using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class UpdateItemModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<MenuItem> itemRepository;

        public UpdateItemModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<MenuItem> itemRepository)
        {
            this.shopRepository = shopRepository;
            this.itemRepository = itemRepository;
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public MenuItem menuItem { get; set; }
        public List<MenuItem> MenuItems { get; set; }


        public async Task<IActionResult> OnGetAsync(int id, int shopId)
        {
            MenuItems = await itemRepository.GetItemIByShopIdAsync(shopId);
            menuItem = await itemRepository.GetMenuItemsByIdAsync(id);
            menuItem.ShopId = shopId;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int shopId)
        {
            menuItem.ShopId = shopId;
            menuItem.ItemEnabled = true;
            await itemRepository.UpdateAsync(menuItem);

            // Cập nhật lại thông tin phân trang
            return RedirectToPage("./MenuItemManager", new { shopId = menuItem.ShopId, pageIndex = 1 });
        }

    }
}