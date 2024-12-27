using Demo_shopping.Data;
using Demo_shopping.Helpers;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)  
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await _context.Categories.OrderByDescending(p=>p.Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesModel categories)
        {            
            if (ModelState.IsValid)
            {
                //Them du lieu
                //categories.Slug = categories.Name.Replace(" ", "-");
                categories.Slug = categories.Name.ToSlug();
                var slug = await _context.Categories.FirstOrDefaultAsync(p => p.Slug == categories.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã có trong database");
                    return View(categories);
                }

                _context.Add(categories);
                await _context.SaveChangesAsync();
                TempData["success"] = "Thêm danh mục thành công";
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
            return View(categories);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            CategoriesModel categories = await _context.Categories.FindAsync(Id);
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoriesModel categories)
        {
            if (ModelState.IsValid)
            {
                //Them du lieu
                categories.Slug = categories.Name.Replace(" ", "-");
                var slug = await _context.Categories.FirstOrDefaultAsync(p => p.Slug == categories.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục đã có trong database");
                    return View(categories);
                }

                _context.Update(categories);
                await _context.SaveChangesAsync();
                TempData["success"] = "Cập nhật danh mục thành công";
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
            return View(categories);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            CategoriesModel categories = await _context.Categories.FindAsync(Id);
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            TempData["success"] = "Danh mục đã xóa";
            return RedirectToAction("Index");
        }
    }
}
