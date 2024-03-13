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
        public void OnGet() {
        }
        public async Task<IActionResult> OnPostAsync() {
            string enteredEmail = useremail;
            string enteredPassword = password;

            // Authenticate user using the authentication repository
            var user = await _signInRepository.SignIn(enteredEmail, enteredPassword);

            if (user != null) {
                return RedirectToPage("/Index");
            } else {
                ModelState.AddModelError(string.Empty, "Username or Password is Incorrect");
                return Page();
            }
        }
    }
}
