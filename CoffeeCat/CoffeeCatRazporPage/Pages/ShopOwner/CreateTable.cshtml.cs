using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class CreateTableModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Table> tableRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public CreateTableModel(ICoffeeShopManagerRepository<Table> tableRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.tableRepository = tableRepository;
            this.areaRepository = areaRepository;
            Tables = new List<Table>();
        }

        [BindProperty]
        public Area area { get; set; }
        [BindProperty]
        public Table table { get; set; }
        public List<Table> Tables { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(int areaId) {
            Authenticate();
            Authorization();

            Tables = await tableRepository.GetTableByAreaIdAsync(areaId);
            // Kiểm tra xem khu vực có tồn tại không
            area = await areaRepository.GetAreaByIdAsync(areaId);
            if (area == null) {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           

           
            var existingTable = await tableRepository.GetTableByNameAsync(table.TableName, area.AreaId);
            if (existingTable != null)
            {
                ErrorMessage = "Table name already exists in this area.";
                return Page();
            }
            

            if (string.IsNullOrWhiteSpace(table.TableName))
            {
                ErrorMessage = "Table name cannot be empty.";
                return Page();
            }

            table.TableEnabled = true;
            table.TableStatus = true;

            await tableRepository.AddAsync(table);

            return RedirectToPage("./TableManager", new { areaId = table.AreaId, pageIndex = 1 });
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
