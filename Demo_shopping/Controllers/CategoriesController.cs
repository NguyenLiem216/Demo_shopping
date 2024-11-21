using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
	public class CategoriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
