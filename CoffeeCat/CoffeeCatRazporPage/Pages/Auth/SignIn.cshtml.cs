using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Auth {

    public class SignInModel : PageModel {
        private readonly ISignInRepository _signInRepository;

        [BindProperty]
        public string useremail { get; set; }

        [BindProperty]
        public string password { get; set; }

        public SignInModel(ISignInRepository signInRepository) {
            _signInRepository = signInRepository;
            useremail = string.Empty;
            password = string.Empty;
        }
        public IActionResult OnGet() {
            // Check login state
            if (HttpContext.Session.GetInt32("UserId") != null) {
                return RedirectToPage("/Home/UserProfile");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync() {
            string enteredEmail = useremail;
            string enteredPassword = password;

            // Check field required
            if (!ModelState.IsValid) {
                return Page();
            }

            var user = await _signInRepository.SignIn(enteredEmail, enteredPassword);

            if (user != null) {
                // Save UserSession
                HttpContext.Session.SetInt32("UserId", user.CustomerId);
                if (user.RoleId.HasValue) {
                    HttpContext.Session.SetInt32("RoleId", user.RoleId.Value);
                }
                if (user.RoleId.HasValue && user.RoleId == 2)
                {
                    HttpContext.Session.SetInt32("OwnerId", user.CustomerId); // L?u ownerId vào session
                }

                // Role Divition
                if (user.RoleId.HasValue) {
                    // Get Role page index
                    string pageIndex = roleDivition(user.RoleId.Value);
                    return RedirectToPage(pageIndex);
                } else {
                    return Page();
                }
            } else {
                // Not username, password have been found!
                ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                return Page();
            }
        }

        private String roleDivition(int role) {
            if (role == 1) {
                return "/Admin/Admin";
            } else if (role == 2) {
                return "/ShopOwner/ShopManager";
            } else if (role == 3) {
                return "/Staff/StaffHomePage";
            } else {
                return "/Customer/CustomerHomePage";
            }
        }
    }
}
