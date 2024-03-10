using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories;

namespace CoffeeCatRazporPage.Pages
{
    public class UpdateCatModel : PageModel
    {
        private readonly ICoffeeShopManagerRepository<Cat> catRepository;
        private readonly ICoffeeShopManagerRepository<Area> areaRepository;

        public UpdateCatModel(ICoffeeShopManagerRepository<Cat> catRepository, ICoffeeShopManagerRepository<Area> areaRepository)
        {
            this.catRepository = catRepository;
            this.areaRepository = areaRepository;
        }

        [BindProperty]
        public Cat cat { get; set; }
        public List<Cat> Cats { get; set; }
        public IFormFile CatImageFile { get; set; }
        public async Task<IActionResult> OnGetAsync(int id, int AreaId)
        {
            Cats = await catRepository.GetCatByAreaIdAsync(AreaId);
            cat = await catRepository.GetCatByIdAsync(id);

            cat.AreaId = AreaId;
            if (!string.IsNullOrEmpty(cat.CatImage))
            {

                cat.CatImage = "Image/" + Guid.NewGuid().ToString() + "_" + CatImageFile.FileName;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int AreaId)
        {
            // Xử lý tệp tin ảnh được tải lên
            if (CatImageFile != null && CatImageFile.Length > 0)
            {
                // Lưu trữ ảnh vào thư mục trên máy chủ
                var imagePath = "Image" + Guid.NewGuid().ToString() + "_" + CatImageFile.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CatImageFile.CopyToAsync(stream);
                }

                // Gán đường dẫn của ảnh vào thuộc tính cat.CatImage
                cat.CatImage = imagePath;
            }

            // Gán các giá trị còn lại cho cat
            cat.AreaId = AreaId;
            cat.CatEnabled = false;

            // Lưu dữ liệu của cat vào cơ sở dữ liệu
            await catRepository.UpdateAsync(cat);

            // Chuyển hướng sau khi cập nhật thành công
            return RedirectToPage("./CatManager", new { areaId = cat.AreaId, pageIndex = 1 });
        }
    }
}