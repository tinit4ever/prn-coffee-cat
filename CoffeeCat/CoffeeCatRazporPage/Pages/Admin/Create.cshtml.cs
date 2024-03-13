using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Admin;

namespace CoffeeCatRazporPage.Pages.Admin {
    [BindProperties]
    public class CreateModel : PageModel {
        private readonly IAdminRepository _adminRepository;

        public User User { get; set; }
        public Shop Shop { get; set; }

        public CreateModel(IAdminRepository adminRepository) {
            _adminRepository = adminRepository;
            User = new User();
            Shop = new Shop();
        }
        public void OnGet() {
        }

        public async Task<IActionResult> OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }
            User.Shop = Shop;
            User.RoleId = 2;
            await _adminRepository.CreateShopOwner(User);
            return RedirectToPage("/Home/Admin");
        }
    }
}
