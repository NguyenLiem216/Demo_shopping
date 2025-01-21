using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Controllers
{
    public class BrandsController : Controller
    {
        private readonly DataContext _context;

        public BrandsController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string Slug = "")
        {
            if (string.IsNullOrWhiteSpace(Slug))
            {
                TempData["error"] = "Slug không hợp lệ.";
                return RedirectToAction("List"); // Ví dụ: Chuyển về danh sách thương hiệu
            }

            var brands = await _context.Brands.FirstOrDefaultAsync(c => c.Slug == Slug);
            if (brands == null)
            {
                TempData["error"] = "Thương hiệu không tồn tại.";
                return View("NotFound");
            }

            var productsByBrand = _context.Products.Where(p => p.BrandId == brands.Id);
            return View(await productsByBrand.OrderByDescending(p => p.Id).ToListAsync());
        }

    }
}
