using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class UpdateCatModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Cat> catRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateCatModel(ICoffeeShopManagerRepository<Cat> catRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.catRepository = catRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Cat cat { get; set; }
        public List<Cat> Cats { get; set; }
        public IFormFile CatImageFile { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int id, int AreaId) {
            Authenticate();
            Authorization();
            Cats = await catRepository.GetCatByAreaIdAsync(AreaId);
            cat = await catRepository.GetCatByIdAsync(id);

            cat.AreaId = AreaId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int AreaId)
        {
            var existingCat = await areaRepository.GetCatByNameAsync(cat.CatName, AreaId);
            if (existingCat != null )
            {
                ErrorMessage = "cat name already exists in this area.";
                return Page();
            }



            cat.AreaId = AreaId;
            cat.CatEnabled = true;


            await catRepository.UpdateAsync(cat);


            return RedirectToPage("./CatManager", new { areaId = cat.AreaId, pageIndex = 1 });
        }

            private void Authenticate() {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) {
                HttpContext.Response.Redirect("/Auth/SignIn");
            }
        }

        private void Authorization() {
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId.HasValue) {
                if (roleId.Value != 2) {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}