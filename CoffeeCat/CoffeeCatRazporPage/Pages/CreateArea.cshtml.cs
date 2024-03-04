using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class CreateAreaModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateAreaModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
            this.Areas = new List<Area>();
        }

        [BindProperty]
        public Shop shop { get; set; }
        [BindProperty]
        public Area area { get; set; }
        public List<Area> Areas { get; set; }

        public async Task<IActionResult> OnGetAsync(int areaId)
        {
            // Lấy danh sách mèo theo areaId
            Areas = await areaRepository.GetAreaByShopIdAsync(1);
            // Kiểm tra xem khu vực có tồn tại không
            area = await areaRepository.GetAreaByIdAsync(1);
            if (area == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            area.AreaEnabled = false;

            await areaRepository.AddAsync(area);


            return RedirectToPage("./AreaManager");
        }
    }
}
