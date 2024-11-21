using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
	public class ProductsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Details()
		{
			return View();
		}
	}
}
