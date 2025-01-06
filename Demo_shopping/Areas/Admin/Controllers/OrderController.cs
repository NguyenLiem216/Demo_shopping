using Demo_shopping.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.OrderByDescending(p=>p.Id).ToListAsync());
        }
        public async Task<IActionResult> ViewOrder(string ordercode)
        {
            var DetailsOrder = await _context.OrderDetails.Include(o=>o.Products).Where(o=>o.OrderCode==ordercode).ToListAsync();
            return View(DetailsOrder);
        }
    }
}
