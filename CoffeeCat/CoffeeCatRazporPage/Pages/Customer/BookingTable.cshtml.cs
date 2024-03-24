using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;
using Repositories.Auth;
using System.ComponentModel.DataAnnotations;

namespace CoffeeCatRazporPage.Pages.Customer {
    public class BookingTableModel : PageModel {
        private readonly ICoffeeShopManagerRepository<Table> tableRepository;
        private readonly ICoffeeShopManagerRepository<Booking> bookingRepository;
        private readonly ISessionRepository sessionrepository;

        public BookingTableModel(ICoffeeShopManagerRepository<Table> tableRepository, ICoffeeShopManagerRepository<Booking> bookingRepository, ISessionRepository sessionRepository) {
            this.tableRepository = tableRepository;
            this.bookingRepository = bookingRepository;
            this.sessionrepository = sessionRepository;
        }

        [BindProperty]
        public List<int> TableIds { get; set; }

        [BindProperty]
        public List<Table> SelectedTables { get; set; }

        [BindProperty]
        [Display(Name = "Booking Start Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime BookingStartTime { get; set; }

        [Display(Name = "Booking End Time")]
        [BindProperty]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm}")]
        public DateTime BookingEndTime { get; set; }

        [BindProperty]
        public List<Table> Tables { get; set; }
        [TempData]
        public int BookingId { get; set; }
        [BindProperty]
        public int AreaId { get; set; }
        [BindProperty]
        public int ShopId { get; set; }
        [BindProperty]
        public bool IsTableSelectionRequired { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGet() {
            Authenticate();
            Authorization();

            IsTableSelectionRequired = true;
            if (Request.Query.TryGetValue("areaId", out var areaId) &&
                Request.Query.TryGetValue("shopId", out var shopId) &&
                Request.Query.TryGetValue("startTime", out var startTime) &&
                Request.Query.TryGetValue("endTime", out var endTime)) {
                AreaId = int.Parse(areaId);
                BookingStartTime = DateTime.Parse(startTime);
                BookingEndTime = DateTime.Parse(endTime);
                ShopId = int.Parse(shopId);

                var availableTables = await bookingRepository.GetAvailableTablesAsync(AreaId, BookingStartTime, BookingEndTime);
                Tables = availableTables.Where(table => table.TableStatus == true && table.TableEnabled == true).ToList();
            }
            if (Tables == null || !Tables.Any())
            {
                ErrorMessage = "Sorry, the table is full.";
                return Page();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync() {
            // Xử lý form booking

            bool areAllTablesAvailable = await CheckAllTablesAvailability(SelectedTables, BookingStartTime, BookingEndTime);
            var customer = sessionrepository.GetUserByRole(HttpContext.Session.GetInt32("UserId"));
            var booking = new Booking {
                BookingCode = GenerateBookingCode(),
                BookingStartTime = BookingStartTime,
                BookingEndTime = BookingEndTime,
                BookingEnabled = false,
                CustomerId = customer.CustomerId
            };

            if (areAllTablesAvailable) {
                await bookingRepository.AddAsync(booking);
                await bookingRepository.AddTablesToBookingAsync(booking.BookingId, TableIds);

                return RedirectToPage("/Customer/BookingMenuItem", new { bookingId = booking.BookingId, shopId = ShopId });
            } else {
                ModelState.AddModelError(string.Empty, "One or more selected tables are not available for the specified date.");
                return Page();
            }
        }
        private string GenerateBookingCode() {
            string code = "BOOK" + DateTime.Now.ToString("yyyyMMddHHmmss");
            return code;
        }

        private async Task<bool> CheckAllTablesAvailability(List<Table> tables, DateTime bookingStartTime, DateTime bookingEndTime) {
            var distinctTables = tables.Distinct().ToList();

            foreach (var table in distinctTables) {
                var existingBooking = await bookingRepository.GetBookingByTableAndTimeAsync(table.TableId, bookingStartTime, bookingEndTime);

                if (existingBooking != null) {
                    table.TableStatus = false;
                    await tableRepository.UpdateAsync(table);
                    return false;
                }
            }

            return true;
        }

        private void Authenticate() {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null) {
                HttpContext.Response.Redirect("/Auth/SignIn");
            }
        }

        private void Authorization() {
            int? roleId = HttpContext.Session.GetInt32("RoleId");
            if (roleId.HasValue) {
                if (roleId.Value != 4) {
                    HttpContext.Response.Redirect("/Error/403");
                }
            }
        }
    }
}
