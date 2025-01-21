using Demo_shopping.Data;
using Demo_shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Demo_shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.OrderByDescending(u => u.Id).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(new AppUserModel());
        }
        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }
            
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }


            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string Id, AppUserModel user )
        {
            var existingUser = await _userManager.FindByIdAsync(Id);
            if (existingUser == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.RoleId = user.RoleId;

                var updateResult = await _userManager.UpdateAsync(existingUser);
                if (updateResult.Succeeded)
                {
                    return RedirectToAction("Index","User");
                }
                else
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(existingUser);
                }
            }
            var roles = await _roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");


            TempData["error"] = "Model validation failed.";
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e=>e.ErrorMessage)).ToList();
            string errorMessage = string.Join("\n", errors);
            
            return View(existingUser);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(AppUserModel user)
		{
			if(ModelState.IsValid)
            {
                var createUserResult = await _userManager.CreateAsync(user,user.PasswordHash);
                if (createUserResult.Succeeded)
                {
                    var createUser = await _userManager.FindByEmailAsync(user.Email);
                    var userId = createUser.Id;
                    var role = _roleManager.FindByIdAsync(user.RoleId);
                    //gán quyền
                    var addToRoleResult = await _userManager.AddToRoleAsync(createUser,role.Result.Name);
                    if(!addToRoleResult.Succeeded)
                    {
                        foreach(var error in createUserResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                    return RedirectToAction("Index", "User");
                }
                else
                {
					foreach (var error in createUserResult.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return View(user);
                }
            }
            else
            {
				TempData["error"] = "Model đang bị lỗi";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach (var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}

				string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
			}

			var roles = await _roleManager.Roles.ToListAsync();
			ViewBag.Roles = new SelectList(roles, "Id", "Name");

			return View(user);
		}
        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                return NotFound();
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                return View("Error");
            }
            TempData["success"] = "Tài khoản đã được xóa thành công";
            return RedirectToAction("Index");

        }
	}
}
