using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer
{
    public class DetailTableModel : PageModel
    {
        private readonly ICustomerRepository<Table> repository;
        private readonly ICustomerRepository<Area> areaRepository;
        public DetailTableModel(ICustomerRepository<Table> repository, ICustomerRepository<Area> areaRepository)
        {
            this.repository = repository;
            this.areaRepository = areaRepository;
        }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Table> Tables { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int AreaId { get; set; }
        public int? ShopId { get; set; }
        public async Task OnGetAsync(int? pageIndex, int AreaId)
        {
            this.AreaId = AreaId;
            IQueryable<Table> tablesQuery = await repository.GetTableEnableAsync();
            var area = await areaRepository.GetShopByIdAsync(AreaId);
            ShopId = area.ShopId;
            tablesQuery = tablesQuery.Where(a => a.AreaId == this.AreaId);

            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await tablesQuery.CountAsync() / (double)pageSize);

            Tables = await tablesQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
