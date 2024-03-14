using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Admin;

namespace CoffeeCatRazporPage.Pages.Home {
    public class AdminModel : PageModel {
        private readonly IAdminRepository _adminRepository;

        public AdminModel(IAdminRepository adminRepository) {
            _adminRepository = adminRepository;
        }
        public IEnumerable<User> Users { get; set; }
        public void OnGet() {
            Users = _adminRepository.GetAllAccount();
        }

        public async Task<IActionResult> OnPostBanAsync(int customerId) {
            await _adminRepository.Ban(customerId);
            return RedirectToPage("/Home/Admin");
        }

        public async Task<IActionResult> OnPostUnbanAsync(int customerId) {
            await _adminRepository.Unban(customerId);
            return RedirectToPage("/Home/Admin");
        }
    }
}
