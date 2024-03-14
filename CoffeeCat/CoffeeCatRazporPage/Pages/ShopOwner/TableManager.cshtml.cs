﻿using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class TableManagerModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Table> repository;

        public TableManagerModel(ICoffeeShopManagerRepository<Table> repository)
        {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<Table> Tables { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<Cat> Cats { get; set; }
        public int AreaId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        public async Task OnGetAsync(int? pageIndex, string sortOrder, int areaId)
        {
            // Lấy danh sách cửa hàng từ repository
            IQueryable<Table> tableQuery = await repository.GetTablesByAreaIdAsync(areaId);
            AreaId = areaId;
            // Tìm kiếm
            if (!string.IsNullOrEmpty(SearchString))
            {
                tableQuery = tableQuery.Where(s => s.TableName.Contains(SearchString));
            }

            // Sắp xếp
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = sortOrder == "Address" ? "address_desc" : "Address";

            switch (sortOrder)
            {

                case "Address":
                    tableQuery = tableQuery.OrderBy(s => s.TableId);
                    break;
                case "address_desc":
                    tableQuery = tableQuery.OrderByDescending(s => s.TableId);
                    break;

            }

            // Phân trang
            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await tableQuery.CountAsync() / (double)pageSize);

            Tables = await tableQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled)
        {
            var table = await repository.GetTableByIdAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            table.TableEnabled = isEnabled;
            await repository.UpdateAsync(table);


            return RedirectToPage(new { areaId = table.AreaId, pageIndex = PageIndex });
        }
    }
}