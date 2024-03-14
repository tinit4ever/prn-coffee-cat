using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class MenuItemManagerModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<MenuItem> repository;

        public MenuItemManagerModel(ICoffeeShopManagerRepository<MenuItem> repository)
        {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ShopId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }



        public async Task OnGetAsync(int? pageIndex, string sortOrder, int shopId)
        {

            IQueryable<MenuItem> menusQuery = await repository.GetMenuItemsByShopIdAsync(shopId);
            ShopId = shopId;
            // Tìm ki?m
            if (!string.IsNullOrEmpty(SearchString))
            {
                menusQuery = menusQuery.Where(s => s.ItemName.Contains(SearchString));
            }

            // S?p x?p
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            switch (sortOrder)
            {

                case "Address":
                    menusQuery = menusQuery.OrderBy(s => s.ItemId);
                    break;
                case "address_desc":
                    menusQuery = menusQuery.OrderByDescending(s => s.ItemId);
                    break;

            }

            // Phân trang
            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await menusQuery.CountAsync() / (double)pageSize);

            MenuItems = await menusQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled)
        {
            var item = await repository.GetMenuItemsByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            item.ItemEnabled = isEnabled;
            await repository.UpdateAsync(item);

            return RedirectToPage(new { shopId = item.ShopId, pageIndex = PageIndex });
        }
    }
}
