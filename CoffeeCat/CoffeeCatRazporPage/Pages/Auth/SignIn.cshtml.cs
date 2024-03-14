using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Auth {

    [BindProperties]
    public class SignInModel : PageModel {
        private readonly ISignInRepository _signInRepository;
        public string useremail { get; set; }

        public string password { get; set; }

        public SignInModel(ISignInRepository signInRepository) {
            _signInRepository = signInRepository;
            useremail = string.Empty;
            password = string.Empty;
        }
        public void OnGet() {
        }
        public async Task<IActionResult> OnPostAsync() {
            string enteredEmail = useremail;
            string enteredPassword = password;

            if (!ModelState.IsValid) {
                return Page();
            }

            var user = await _signInRepository.SignIn(enteredEmail, enteredPassword);

            if (user != null) {
                Console.WriteLine(user.RoleId);
                int roleId = user.RoleId ?? 3;
                string pageIndex = roleDivition(roleId);
                return RedirectToPage(pageIndex);
            } else {
                ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                return Page();
            }
        }

        private String roleDivition(int role) {
            if (role == 1) {
                return "/Home/Admin";
            } else if (role == 2) {
                return "/Home/ShopOwner";
            } else {
                return "/Home/Customer";
            }
        }
    }
}
