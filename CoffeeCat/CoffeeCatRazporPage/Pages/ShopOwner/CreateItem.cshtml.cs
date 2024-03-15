using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class CreateItemModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<MenuItem> itemRepository;

        public CreateItemModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<MenuItem> itemRepository)
        {
            this.shopRepository = shopRepository;
            this.itemRepository = itemRepository;
            MenuItems = new List<MenuItem>();
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public MenuItem menuItem { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int ShopId)
        {
            // L?y danh sách mèo theo areaId
            MenuItems = await itemRepository.GetItemIByShopIdAsync(ShopId);

            menuItem = await itemRepository.GetMenuItemsByIdAsync(ShopId);
            if (menuItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*    if (!ModelState.IsValid)
                {
                    return Page();
                }*/
            menuItem.ItemEnabled = true;

            await itemRepository.AddAsync(menuItem);


            return RedirectToPage("./AreaManager", new { shopId = menuItem.ShopId, pageIndex = 1 });
        }
    }
}
