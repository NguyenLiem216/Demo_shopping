using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
	public class CartsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
