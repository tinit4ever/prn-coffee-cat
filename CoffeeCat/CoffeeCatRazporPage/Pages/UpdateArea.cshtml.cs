using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class UpdateAreaModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateAreaModel(ICoffeeShopManagerRepository<Cat> catRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Area area { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            area = await areaRepository.GetAreaByIdAsync(1);

            if (area == null)
            {
                return NotFound();
            }

            // Kh?i t?o ShopName n?u nó là null
            if (area.AreaName == null)
            {
                area.AreaName = "";
            }


            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            area.AreaEnabled = false;
            await areaRepository.UpdateAsync(area);



            return RedirectToPage("./AreaManager");
        }
    }
}
