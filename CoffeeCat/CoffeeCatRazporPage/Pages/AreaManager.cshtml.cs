using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class AreaManagerModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Area> repository;

        public AreaManagerModel(ICoffeeShopManagerRepository<Area> repository)
        {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<Area> Areas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageIndex, string sortOrder, int areaId)
        {
            // Lấy danh sách cửa hàng từ repository
            IQueryable<Area> areasQuery = await repository.GetAreasByShopIdAsync(1);

            // Tìm kiếm
            if (!string.IsNullOrEmpty(SearchString))
            {
                areasQuery = areasQuery.Where(s => s.AreaName.Contains(SearchString));
            }

            // Sắp xếp
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            switch (sortOrder)
            {
               
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

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled)
        {
            var area = await repository.GetAreaByIdAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            area.AreaEnabled = isEnabled;
            await repository.UpdateAsync(area);

            return RedirectToPage();
        }
    }
}
