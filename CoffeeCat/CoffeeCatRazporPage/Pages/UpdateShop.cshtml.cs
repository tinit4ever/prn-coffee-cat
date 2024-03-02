using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeCatRazporPage.Pages
{
    public class UpdateShopModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Table> tableRepository;

        public UpdateShopModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Table> tableRepository)
        {
            this.shopRepository = shopRepository;
            this.tableRepository = tableRepository;
        }

        [BindProperty]
        public Shop Shop { get; set; }

        [BindProperty]
        public List<Table> Tables { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Shop = await shopRepository.GetShopByIdAsync(id);

            if (Shop == null)
            {
                return NotFound();
            }

            // Kh?i t?o ShopName n?u nó là null
            /* if (Shop.ShopName == null)
             {
                 Shop.ShopName = "";
             }

             Tables = await tableRepository.GetByShopIdAsync(id);

             return Page();*/
            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }*/

            await shopRepository.UpdateAsync(Shop);

            foreach (var table in Tables)
            {
                await tableRepository.UpdateAsync(table);
            }

            return RedirectToPage("./ShopManager");
        }
    }
}
