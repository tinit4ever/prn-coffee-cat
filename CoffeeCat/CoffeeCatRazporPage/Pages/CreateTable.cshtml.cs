using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class CreateTableModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Table> tableRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateTableModel(ICoffeeShopManagerRepository<Table> tableRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.tableRepository = tableRepository;
            this.areaRepository = areaRepository;
            this.Tables = new List<Table>();
        }

        [BindProperty]
        public Area area { get; set; }
        [BindProperty]
        public Table table { get; set; }
        public List<Table> Tables { get; set; }

        public async Task<IActionResult> OnGetAsync(int areaId)
        {
           
            Tables = await tableRepository.GetTableByAreaIdAsync(areaId);
            // Kiểm tra xem khu vực có tồn tại không
            area = await areaRepository.GetAreaByIdAsync(areaId);
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
            table.TableEnabled = false;
            table.TableStatus = true;

            await tableRepository.AddAsync(table);


            return RedirectToPage("./TableManager",  new { areaId = table.AreaId, pageIndex = 1 });

        }
    }
}
