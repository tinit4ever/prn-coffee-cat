using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CoffeeCatRazporPage.Pages
{
    public class CreateShopModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> shopRepository;
        private readonly ICoffeeShopManagerRepository<Table> tableRepository;

        public CreateShopModel(ICoffeeShopManagerRepository<Shop> shopRepository, ICoffeeShopManagerRepository<Table> tableRepository)
        {
            this.shopRepository = shopRepository;
            this.tableRepository = tableRepository;
            this.Tables = new List<Table>();
        }

        [BindProperty]
        public Shop Shop { get; set; }
        public bool ShopEnabled { get; set; }
        [BindProperty]
        public List<Table> Tables { get; set; } // Để tạo một danh sách các bàn
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Shop.ShopEnabled = false;
            await shopRepository.AddAsync(Shop);

            // Thêm danh sách các bàn
          /*  foreach (var table in Tables)
            {
                table.ShopId = Shop.ShopId;
                table.TableEnabled = true;
                // Gán id của cửa hàng cho từng bàn
                await tableRepository.AddAsync(table);
            }*/

            return RedirectToPage("./ShopManage");
        }
    }
}
