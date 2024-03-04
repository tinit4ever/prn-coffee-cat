using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class CreateCatModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Cat> catRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateCatModel(ICoffeeShopManagerRepository<Cat> catRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.catRepository = catRepository;
            this.areaRepository = areaRepository;
            this.Cats = new List<Cat>();
        }

        [BindProperty]
        public Area area { get; set; }
        [BindProperty]
        public Cat Cat { get; set; }
        public List<Cat> Cats { get; set; }

        public async Task<IActionResult> OnGetAsync(int areaId)
        {
            // Lấy danh sách mèo theo areaId
            Cats = await catRepository.GetCatByAreaIdAsync(1);
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
            Cat.CatEnabled = false;

            await catRepository.AddAsync(Cat);


            return RedirectToPage("./ShopManager");
        }
    }
}
