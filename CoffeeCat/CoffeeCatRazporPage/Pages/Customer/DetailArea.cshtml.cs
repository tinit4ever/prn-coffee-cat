using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer
{
    public class DetailAreaModel : PageModel
    {
        private readonly ICustomerRepository<Area> areaRepository;
        private readonly ICustomerRepository<Shop> repository;

        public DetailAreaModel(ICustomerRepository<Area> areaRepository, ICustomerRepository<Shop> repository)
        {
            this.areaRepository = areaRepository;
            this.repository = repository;
        }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<Area> Areas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        // Thêm thu?c tính ShopName
        public int ShopId { get; set; }
        public async Task OnGetAsync(int ShopId, int? pageIndex)
        {

            this.ShopId = ShopId;
            IQueryable<Area> areasQuery = await areaRepository.GetAreaEnableAsync();

            // shopsQuery = shopsQuery.Where(s => s.ShopName.Contains(SearchString));
            areasQuery = areasQuery.Where(a => a.ShopId == this.ShopId);

            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await areasQuery.CountAsync() / (double)pageSize);

            Areas = await areasQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        }
    }
}
