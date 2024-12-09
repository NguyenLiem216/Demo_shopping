using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            ViewBag.Brands = new SelectList(_context.Brands, "Id", "Name", products.BrandId);

            if (ModelState.IsValid)
            {
                //Them du lieu
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

            return View(products);
        }
    }
}
