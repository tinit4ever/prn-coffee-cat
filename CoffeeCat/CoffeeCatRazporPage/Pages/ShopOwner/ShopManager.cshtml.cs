using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;
using System.Threading.Tasks;

namespace CoffeeCatRazporPage.Pages.ShopOwner
{
    public class ShopManager : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Shop> repository;
        private readonly ISessionRepository sessionRepository;

        public ShopManager(ICoffeeShopManagerRepository<Shop> repository, ISessionRepository sessionRepository)
        {
            this.repository = repository;
            this.sessionRepository = sessionRepository;
        }

        [BindProperty(SupportsGet = true)]
        public Shop Shop { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!Authenticate())
            {
                return RedirectToSignIn();
            }

            if (!Authorize())
            {
                return RedirectToError403();
            }

            var shopOwner = sessionRepository.GetUserByRole( HttpContext.Session.GetInt32("UserId"));
            if (shopOwner == null)
            {
                return RedirectToSignIn(); // Redirect to sign-in if shop owner not found
            }

            Shop = await repository.GetShopByIdAsync(shopOwner.ShopId);
            return Page();
        }

        public async Task<IActionResult> OnPostToggleEnabledAsync(int id, bool isEnabled)
        {
            var shop = await repository.GetShopByIdAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            shop.ShopEnabled = isEnabled;
            await repository.UpdateAsync(shop);
            return RedirectToPage();
        }

        private bool Authenticate()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            return userId != null;
        }

        private bool Authorize()
        {
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            return roleId.HasValue && roleId.Value == 2;
        }

        private IActionResult RedirectToSignIn()
        {
            return RedirectToPage("/Auth/SignIn");
        }

        private IActionResult RedirectToError403()
        {
            return RedirectToPage("/Error/403");
        }
    }
}
