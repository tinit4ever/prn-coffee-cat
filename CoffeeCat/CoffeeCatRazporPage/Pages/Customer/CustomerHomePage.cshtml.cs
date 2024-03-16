using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer {
    public class CustomerModel : PageModel {
        private readonly ICustomerRepository<Shop> repository;

        public CustomerModel(ICustomerRepository<Shop> repository) {
            this.repository = repository;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public IEnumerable<Shop> Shops { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string ShopName { get; set; }

        public async Task OnGetAsync(int? pageIndex, string sortOrder) {
       
            IQueryable<Shop> shopsQuery = await repository.GetShopEnableAsync();

          
            if (!string.IsNullOrEmpty(SearchString)) {
                shopsQuery = shopsQuery.Where(s => s.ShopName.Contains(SearchString));
            }

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
    }
}
