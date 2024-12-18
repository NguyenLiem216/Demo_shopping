using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
