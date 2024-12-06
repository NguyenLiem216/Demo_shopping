using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                            .Include(p => p.Category)
                            .Include(p => p.Brand)
                            .OrderByDescending(p => p.Id)
                            .ToListAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name");

            return View();
        }
        public async Task<IActionResult> Create(ProductsModel products)
        {
			ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", products.CategoryId);
			ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name",products.BrandId);
            
            return View(products);
		}
	}
}
