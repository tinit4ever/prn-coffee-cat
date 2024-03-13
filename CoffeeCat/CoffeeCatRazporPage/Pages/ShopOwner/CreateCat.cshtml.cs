using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class CreateCatModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Cat> catRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateCatModel(ICoffeeShopManagerRepository<Cat> catRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.catRepository = catRepository;
            this.areaRepository = areaRepository;
            Cats = new List<Cat>();
        }

        [BindProperty]
        public Area area { get; set; }
        [BindProperty]
        public Cat Cat { get; set; }
        public List<Cat> Cats { get; set; }

        public async Task<IActionResult> OnGetAsync(int areaId)
        {

            Cats = await catRepository.GetCatByAreaIdAsync(areaId);

            area = await areaRepository.GetAreaByIdAsync(areaId);
            if (area == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*      if (!ModelState.IsValid)
                  {
                      return Page();
                  }*/
            Cat.CatEnabled = false;
            await catRepository.AddAsync(Cat);


            return RedirectToPage("./CatManager", new { areaId = area.AreaId, pageIndex = 1 });

        }
    }
}
