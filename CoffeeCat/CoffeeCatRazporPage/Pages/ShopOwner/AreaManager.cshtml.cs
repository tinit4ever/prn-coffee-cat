using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class AreaManagerModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Area> repository;
        private readonly ISessionRepository sessionrepository;
        public AreaManagerModel(ICoffeeShopManagerRepository<Area> repository, ISessionRepository sessionrepository) {
            this.repository = repository;
            this.sessionrepository = sessionrepository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<Area> Areas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ShopId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }



        public async Task OnGetAsync(int? pageIndex, string sortOrder, int shopId) {
            Authenticate();
            Authorization();
            var shopOwner = sessionrepository.GetUserByRole( HttpContext.Session.GetInt32("UserId"));
            IQueryable<Area> areasQuery = await repository.GetAreasByShopIdAsync(shopOwner.ShopId);
            ShopId = shopId;
            // Tìm kiếm
            if (!string.IsNullOrEmpty(SearchString)) {
                areasQuery = areasQuery.Where(s => s.AreaName.Contains(SearchString));
            }

            // Sắp xếp
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            switch (sortOrder) {

                case "Address":
                    areasQuery = areasQuery.OrderBy(s => s.AreaId);
                    break;
                case "address_desc":
                    areasQuery = areasQuery.OrderByDescending(s => s.AreaId);
                    break;

            }

            // Phân trang
            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await areasQuery.CountAsync() / (double)pageSize);

            Areas = await areasQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled) {
            var area = await repository.GetAreaByIdAsync(id);

            if (area == null) {
                return NotFound();
            }

            area.AreaEnabled = isEnabled;
            await repository.UpdateAsync(area);

            return RedirectToPage(new { shopId = area.ShopId, pageIndex = PageIndex });
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
