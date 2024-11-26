using Demo_shopping.Data;
using Demo_shopping.Helpers;
using Demo_shopping.Models;
using Demo_shopping.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
    public class CartsController : Controller
    {
        private readonly DataContext _context;

        public CartsController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartVM = new()
            {
                cartItem = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }
        public async Task<IActionResult> Add(int Id)
        {
            ProductsModel products = await _context.Products.FindAsync(Id);
            List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItems = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            if (cartItems == null)
            {
                cart.Add(new CartItemModel(products));
            }
            else
            {
                cartItems.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart",cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
