using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Auth;
using System.ComponentModel.DataAnnotations;

namespace CoffeeCatRazporPage.Pages.Auth {
    [BindProperties]
    public class RegisterModel : PageModel {
        private readonly IRegisterRepository _registerRepository;

        public string name { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string useremail { get; set; }

        [Display(Name = "Password")]
        [StringLength(23, ErrorMessage = "Range must be from 1 to 23")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one number, and one special character")]
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public RegisterModel(IRegisterRepository registerRepository) {
            _registerRepository = registerRepository;
            name = string.Empty;
            phone = string.Empty;
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
            userToCreate.RoleId = 4;

            // Dynamic value
            userToCreate.CustomerName = name;
            userToCreate.CustomerTelephone = phone;
            userToCreate.CustomerEmail = useremail;
            userToCreate.CustomerPassword = password;

            try {
                await _registerRepository.RegisterAsync(userToCreate);
                Console.WriteLine("User created successfully.");

                // Save UserSession
                HttpContext.Session.SetInt32("UserId", userToCreate.CustomerId);
                HttpContext.Session.SetInt32("RoleId", userToCreate.RoleId.Value);

                // Redirect To Home
                return RedirectToPage("/Customer/CustomerHomePage");
            } catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
