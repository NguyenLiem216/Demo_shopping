﻿using Microsoft.AspNetCore.Mvc;

namespace Demo_shopping.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
