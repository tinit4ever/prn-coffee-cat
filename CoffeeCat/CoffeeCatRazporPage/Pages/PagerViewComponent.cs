using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoffeeCatRazporPage.Pages
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int totalPages, int currentPage)
        {
            // Tạo một model cho View Component của bạn nếu cần
            var model = new { TotalPages = totalPages, CurrentPage = currentPage };

            // Trả về view tương ứng với View Component và truyền model vào
            return View(model);
        }
    }
}