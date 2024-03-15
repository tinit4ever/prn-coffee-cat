using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;
using System.ComponentModel.DataAnnotations;

namespace CoffeeCatRazporPage.Pages.Home {
    public class UserProfileModel : PageModel {
        private readonly ISessionRepository _sessionRepository;
        private readonly IProfileRepository _profileRepository;

        public string Mail { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public string Phone { get; set; }

        [BindProperty]
        [StringLength(23, ErrorMessage = "Range must be from 8 to 23")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one number, and one special character")]
        public string Password { get; set; }
        public User User { get; set; }

        public UserProfileModel(IProfileRepository profileRepository, ISessionRepository sessionRepository) {
            _profileRepository = profileRepository;
            _sessionRepository = sessionRepository;
        }
        public void OnGet() {
            SetUser();
            Mail = User.CustomerEmail;
            UserName = User.CustomerName;
            Phone = User.CustomerTelephone ?? "";
            Password = User.CustomerPassword;
        }

        public void OnPostLogout() {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("RoleId");
            HttpContext.Response.Redirect("/Index");
            /*return RedirectToPage("/Index");*/
        }

        public IActionResult OnPostSave() {
            SetUser();

            if (!ModelState.IsValid) {
                return Page();
            }

            User.CustomerName = UserName;
            User.CustomerTelephone = Phone;
            User.CustomerPassword = Password;
            _profileRepository.UpdateUser(User);
            return Page();
        }

        private void SetUser() {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue) {
                User = _sessionRepository.GetUserById(userId.Value);
            }
        }
    }
}
