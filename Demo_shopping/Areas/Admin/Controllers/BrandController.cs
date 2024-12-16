using Demo_shopping.Data;
using Demo_shopping.Helpers;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly DataContext _context;

        public BrandController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.OrderByDescending(b=>b.Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandsModel brands)
        {
            if (ModelState.IsValid)
            {
                //Them du lieu
                //categories.Slug = categories.Name.Replace(" ", "-");
                brands.Slug = brands.Name.ToSlug();
                var slug = await _context.Brands.FirstOrDefaultAsync(p => p.Slug == brands.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thương hiệu đã có trong database");
                    return View(brands);
                }

                _context.Add(brands);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm thương hiệu thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(brands);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            BrandsModel brands = await _context.Brands.FindAsync(Id);
            return View(brands);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandsModel brands)
        {
            if (ModelState.IsValid)
            {
                //Them du lieu
                brands.Slug = brands.Name.Replace(" ", "-");
                var slug = await _context.Brands.FirstOrDefaultAsync(p => p.Slug == brands.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Thương mục đã có trong database");
                    return View(brands);
                }

                _context.Update(brands);
                await _context.SaveChangesAsync();
                TempData["success"] = "Cập nhật thương hiệu thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(brands);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            BrandsModel brands = await _context.Brands.FindAsync(Id);
            _context.Brands.Remove(brands);
            await _context.SaveChangesAsync();
            TempData["success"] = "Thương hiệu đã xóa";
            return RedirectToAction("Index");
        }
    }
}
