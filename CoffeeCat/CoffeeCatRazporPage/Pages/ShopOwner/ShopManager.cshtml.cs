using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class ShopManager : PageModel {
        private readonly ICoffeeShopManagerRepository<Shop> repository;

        public ShopManager(ICoffeeShopManagerRepository<Shop> repository) {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<Shop> Shops { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageIndex, string sortOrder) {
            Authenticate();
            Authorization();
            // Lấy danh sách cửa hàng từ repository
            IQueryable<Shop> shopsQuery = await repository.GetAllAsync();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(SearchString)) {
                shopsQuery = shopsQuery.Where(s => s.ShopName.Contains(SearchString));
            }

            // Sắp xếp
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            switch (sortOrder) {
                case "name_desc":
                    shopsQuery = shopsQuery.OrderByDescending(s => s.ShopName);
                    break;
                case "Address":
                    shopsQuery = shopsQuery.OrderBy(s => s.ShopAddress);
                    break;
                case "address_desc":
                    shopsQuery = shopsQuery.OrderByDescending(s => s.ShopAddress);
                    break;
                default:
                    shopsQuery = shopsQuery.OrderBy(s => s.ShopName);
                    break;
            }

            // Phân trang
            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await shopsQuery.CountAsync() / (double)pageSize);

            Shops = await shopsQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled) {
            var shop = await repository.GetShopByIdAsync(id);

            if (shop == null) {
                return NotFound();
            }

            shop.ShopEnabled = isEnabled;
            await repository.UpdateAsync(shop);

            return RedirectToPage();
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
