using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class StaffManagerModel : PageModel
    {
        private readonly ICoffeeShopStaffRepository _staffRepository;
        public StaffManagerModel(ICoffeeShopStaffRepository coffeeShopStaffRepository)
        {
            _staffRepository = coffeeShopStaffRepository;
        }
        public List<User> UserList { get; set; }
        public async Task OnGetAsync(int roleId)

        {
            Authenticate();
          /*  Authorization();*/

            UserList = await _staffRepository.GetUserbyRold(3);
        }
        public async Task<IActionResult> OnPostBanAsync(int customerId)
        {
            await _staffRepository.Ban(customerId);
            return RedirectToPage("/ShopOwner/StaffManager");
        }

        public async Task<IActionResult> OnPostUnbanAsync(int customerId)
        {
            await _staffRepository.Unban(customerId);
            return RedirectToPage("/ShopOwner/StaffManager");
        }
        private void Authenticate()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                HttpContext.Response.Redirect("/Auth/SignIn");
            }
        }

    /*    private void Authorization()
        {
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId.HasValue)
            {
                if (roleId.Value != 1)
                {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }*/
    }
}
