﻿using Microsoft.AspNetCore.Identity;

namespace Demo_shopping.Models
{
	public class AppUserModel : IdentityUser
	{
		public string Occupation {  get; set; }
	}
}
