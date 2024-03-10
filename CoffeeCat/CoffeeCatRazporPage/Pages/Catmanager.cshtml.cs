﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class CatmanagerModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Cat> repository;

        public CatmanagerModel(ICoffeeShopManagerRepository<Cat> repository)
        {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<Cat> Cats { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int AreaId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public async Task OnGetAsync(int? pageIndex, string sortOrder, int areaId)
        {
            // Lấy danh sách cửa hàng từ repository
            IQueryable<Cat> catsQuery = await repository.GetCatsByAreaIdAsync(areaId);
            AreaId = areaId;
            // Tìm kiếm
            if (!string.IsNullOrEmpty(SearchString))
            {
                catsQuery = catsQuery.Where(s => s.CatName.Contains(SearchString));
            }

            // Sắp xếp
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            switch (sortOrder)
            {
                case "name_desc":
                    catsQuery = catsQuery.OrderByDescending(s => s.CatName);
                    break;
                case "Address":
                    catsQuery = catsQuery.OrderBy(s => s.CatId);
                    break;
                case "address_desc":
                    catsQuery = catsQuery.OrderByDescending(s => s.CatId);
                    break;
                default:
                    catsQuery = catsQuery.OrderBy(s => s.CatName);
                    break;
            }

            // Phân trang
            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await catsQuery.CountAsync() / (double)pageSize);

            Cats = await catsQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled)
        {
            var cat = await repository.GetCatByIdAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            cat.CatEnabled = isEnabled;
            await repository.UpdateAsync(cat);

            return RedirectToPage(new { areaId = cat.AreaId, pageIndex = PageIndex });
        }
    }
}
