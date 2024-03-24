using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;
using System.ComponentModel.DataAnnotations;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    [BindProperties]
    public class CreateStaffModel : PageModel
    {
        private readonly ICoffeeShopStaffRepository _coffeeShopStaffRepository;
        private readonly ISessionRepository _sessionRepository;
        /*public string name { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string useremail { get; set; }

        [Display(Name = "Password")]
        [StringLength(23, ErrorMessage = "Range must be from 1 to 23")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one number, and one special character")]
        public string password { get; set; }
        public string confirmPassword { get; set; }*/
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter a valid 10-digit phone number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string useremail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [StringLength(23, ErrorMessage = "Range must be from 1 to 23")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "Password must contain at least 8 characters, including one uppercase letter, one lowercase letter, one number, and one special character")]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
   /*     [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]*/
        public string confirmPassword { get; set; }

        public CreateStaffModel(ICoffeeShopStaffRepository coffeeShopStaffRepository, ISessionRepository sessionRepository)
        {
            _coffeeShopStaffRepository = coffeeShopStaffRepository;
            _sessionRepository = sessionRepository; 
            name = string.Empty;
            phone = string.Empty;
            useremail = string.Empty;
            password = string.Empty;
            confirmPassword = string.Empty;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (password != confirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Confirm password is not match");
                return Page();
            }

            if (_coffeeShopStaffRepository.IsExistedEmail(useremail))
            {
                ModelState.AddModelError(string.Empty, "Email is have been existed");
                return Page();
            }

            User userToCreate = new User();
            // Default value
            userToCreate.CustomerEnabled = true;
            userToCreate.RoleId = 3;

            // Dynamic value
            userToCreate.CustomerName = name;
            userToCreate.CustomerTelephone = phone;
            userToCreate.CustomerEmail = useremail;
            userToCreate.CustomerPassword = password;
            var shopOwner = _sessionRepository.GetUserByRole(2);
            userToCreate.ShopId = shopOwner.ShopId;
            try
            {
                await _coffeeShopStaffRepository.CreateStaff(userToCreate);
                Console.WriteLine("User created successfully.");

                // Save UserSession
              /*  HttpContext.Session.SetInt32("UserId", userToCreate.CustomerId);
                HttpContext.Session.SetInt32("RoleId", userToCreate.RoleId.Value);*/

                // Redirect To Home
                return RedirectToPage("/ShopOwner/StaffManager");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}

