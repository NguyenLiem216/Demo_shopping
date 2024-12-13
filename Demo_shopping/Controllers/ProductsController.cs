using Demo_shopping.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int Id)
        {
            if (Id == 0)
            {
                return RedirectToAction("Index");
            }
            var productsById = _context.Products.Where(p => p.Id == Id).FirstOrDefault();

            return View(productsById);

        }
    }
}
