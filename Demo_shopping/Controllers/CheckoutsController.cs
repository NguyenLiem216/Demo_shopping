using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
    public class CheckoutsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
