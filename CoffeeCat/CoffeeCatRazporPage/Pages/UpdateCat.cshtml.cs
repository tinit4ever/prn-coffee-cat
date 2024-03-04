using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class UpdateCatModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Cat> catRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateCatModel(ICoffeeShopManagerRepository<Cat> catRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.catRepository = catRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Cat cat { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            cat = await catRepository.GetCatByIdAsync(1);

            if (cat == null)
            {
                return NotFound();
            }

            // Kh?i t?o ShopName n?u n� l� null
            if (cat.CatName == null)
            {
                cat.CatName = "";
            }


            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            cat.CatEnabled = false;
            await catRepository.UpdateAsync(cat);



            return RedirectToPage("./CatManager");
        }
    }
}
