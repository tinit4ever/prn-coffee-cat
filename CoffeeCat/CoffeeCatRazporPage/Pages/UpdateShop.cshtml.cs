using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Core.Types;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeCatRazporPage.Pages
{
    public class UpdateShopModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateShopModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.shopRepository = shopRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Shop Shop { get; set; }






        public async Task<IActionResult> OnGetAsync(int id)
        {
            Shop = await shopRepository.GetShopByIdAsync(id);

            if (Shop == null)
            {
                return NotFound();
            }

            // Kh?i t?o ShopName n?u nó là null
            if (Shop.ShopName == null)
            {
                Shop.ShopName = "";
            }

         
            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            Shop.ShopEnabled = false;
            await shopRepository.UpdateAsync(Shop);



            return RedirectToPage("./ShopManager", new {  pageIndex = 1 });
        }
    }
}
