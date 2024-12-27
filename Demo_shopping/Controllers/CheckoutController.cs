using Demo_shopping.Data;
using Demo_shopping.Helpers;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo_shopping.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _context;

		public CheckoutController(DataContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}


		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login","Account");
			}
			else
			{
				var ordercode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = ordercode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1;
				orderItem.CreatedDate = DateTime.UtcNow;
				_context.Add(orderItem);
				_context.SaveChanges();
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var cart in cartItems)
				{
					var orderdetails = new OrderDetailsModel();
					orderdetails.UserName = userEmail;
					orderdetails.OrderCode = ordercode;
					orderdetails.ProductId = cart.ProductId;
					orderdetails.Price = cart.Price;
					orderdetails.Quantity = cart.Quantity;
					_context.Add(orderdetails);
					_context.SaveChanges();
				}
				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Đặt hàng thành công , vui lòng chờ duyệt đơn hàng";
				return RedirectToAction("Index","Cart");
			}
			return View();
		}
	}
}
