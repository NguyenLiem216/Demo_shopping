using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            CategoriesModel categories = _context.Categories.Where(c => c.Slug == Slug).FirstOrDefault();
            if (categories == null) return RedirectToAction("Index");
            var productsByCategory = _context.Products.Where(p => p.CategoryId == categories.Id);
            return View(await productsByCategory.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
