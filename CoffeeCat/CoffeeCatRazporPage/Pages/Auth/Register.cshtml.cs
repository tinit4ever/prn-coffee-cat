using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;

namespace CoffeeCatRazporPage.Pages.Auth {
    [BindProperties]
    public class RegisterModel : PageModel {
        private readonly IRegisterRepository _registerRepository;

        public string name { get; set; }
        public string useremail { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public RegisterModel(IRegisterRepository registerRepository) {
            _registerRepository = registerRepository;
            name = string.Empty;
            useremail = string.Empty;
            password = string.Empty;
            confirmPassword = string.Empty;
        }
        public void OnGet() {
        }
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            if (password != confirmPassword) {
                ModelState.AddModelError(string.Empty, "Confirm password is not match");
                return Page();
            }

            if (_registerRepository.IsExistedEmail(useremail)) {
                ModelState.AddModelError(string.Empty, "Email is have been existed");
                return Page();
            }

            User userToCreate = new User();
            // Default value
            userToCreate.CustomerEnabled = true;
            userToCreate.RoleId = 3;

            // Dynamic value
            userToCreate.CustomerName = name;
            userToCreate.CustomerEmail = useremail;
            userToCreate.CustomerPassword = password;

            try {
                await _registerRepository.RegisterAsync(userToCreate);
                Console.WriteLine("User created successfully.");
                return RedirectToPage("/Home/Customer");
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
