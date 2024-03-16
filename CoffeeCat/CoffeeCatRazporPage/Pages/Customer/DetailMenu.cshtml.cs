using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer
{
    public class DetailMenuModel : PageModel
    {
        private readonly ICustomerRepository<MenuItem> repository;
        public DetailMenuModel(ICustomerRepository<MenuItem> repository)
        {
            this.repository = repository;
        }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<MenuItem> MenuItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        // Thêm thu?c tính ShopName
        public int ShopId { get; set; }
        public async Task OnGetAsync(int ShopId, int? pageIndex)
        {
            this.ShopId = ShopId;
            IQueryable<MenuItem> menuQuery = await repository.GetMenuItemEnableAsync();
            menuQuery = menuQuery.Where(a => a.ShopId == this.ShopId);

            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await menuQuery.CountAsync() / (double)pageSize);

            MenuItems = await menuQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        }
    }
}
