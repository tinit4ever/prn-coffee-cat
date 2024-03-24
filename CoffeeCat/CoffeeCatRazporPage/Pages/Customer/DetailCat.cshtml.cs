using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer
{
    public class DetailCatModel : PageModel
    {
        private readonly ICustomerRepository<Cat> repository;
        private readonly ICustomerRepository<Area> areaRepository;
        public DetailCatModel(ICustomerRepository<Cat> repository, ICustomerRepository<Area> areaRepository)
        {
            this.repository = repository;
            this.areaRepository = areaRepository;
        }
        [BindProperty(SupportsGet = true)]
        public IEnumerable<Cat> Cats { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int AreaId { get; set; }
        public int? ShopId { get; set; }
        public async Task OnGetAsync(int? pageIndex, int AreaId)
        {
            this.AreaId = AreaId;
            IQueryable<Cat> catsQuery = await repository.GetCatEnableAsync();
            var area = await areaRepository.GetShopByIdAsync(AreaId);
            ShopId = area.ShopId;
            catsQuery = catsQuery.Where(a => a.AreaId == this.AreaId);

            int pageSize = 5;
            CurrentPage = pageIndex ?? 1;
            TotalPages = (int)Math.Ceiling(await catsQuery.CountAsync() / (double)pageSize);

            Cats = await catsQuery.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
