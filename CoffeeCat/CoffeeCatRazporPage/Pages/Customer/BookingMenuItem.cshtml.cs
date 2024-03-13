using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages.Customer
{
    public class BookingMenuItemModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<MenuItem> menuItemRepository;

        public BookingMenuItemModel(ICoffeeShopManagerRepository<MenuItem> menuItemRepository)
        {
            this.menuItemRepository = menuItemRepository;
        }

        [TempData]
        public int BookingId { get; set; }
        [BindProperty]
        public List<int> menuItemIds { get; set; }
        public int ShopId { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public async Task<IActionResult> OnGetAsync(int bookingId,int shopId)
        {
            BookingId = bookingId;
            ShopId = shopId;
            MenuItems = await menuItemRepository.GetAllMenuItemByShopIdAsync(shopId);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {


            await menuItemRepository.AddMenuItemsToBookingAsync(BookingId, menuItemIds);


            return RedirectToPage("/Customer/BookingHistory");
        }
    }
}