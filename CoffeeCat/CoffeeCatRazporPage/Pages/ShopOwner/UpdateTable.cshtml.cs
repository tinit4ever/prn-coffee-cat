using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner {
    public class UpdateTableModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Table> tableRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateTableModel(ICoffeeShopManagerRepository<Table> tableRepository, ICoffeeShopManagerRepository<Area> areaRepository) {
            this.tableRepository = tableRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Table table { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int AreaId) {
            Authenticate();
            Authorization();
            table = await tableRepository.GetTableByIdAsync(id);

            table.AreaId = AreaId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int AreaId) {

            table.AreaId = AreaId;
            table.TableEnabled = false;
            await tableRepository.UpdateAsync(table);



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
