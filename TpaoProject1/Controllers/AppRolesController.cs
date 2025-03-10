﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TpaoProject1.Data;
using Microsoft.EntityFrameworkCore;
using TpaoProject1.Models;
using TpaoProject1.Areas.Identity.Data;

namespace TpaoProject1.Controllers
{
	public class AppRolesController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(UserManager<ApplicationUser> userManager, DatabaseContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
		{

			return View();
		}

		[HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create(IdentityRole model)
		{
			if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
			}
			return RedirectToAction("Index");
		}
		//[HttpGet]
		//public IActionResult AssignRole()
		//{

		//	var users = _dbContext.Users.ToList();
		//	var roles = _roleManager.Roles.ToList();

		//	var viewModel = new UserRolesViewModel
		//	{
		//		Kullanicilar = users,
		//		Roller = roles
		//	};

		//	return View(viewModel);
		//}

		
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(string role,string userid)

        {
			if( userid != null) {

				var currentUser = await _userManager.GetUserAsync(HttpContext.User);
				var changeUser = _userManager.Users.First(x => x.Id == userid);

				
					var currentRole = await _userManager.GetRolesAsync(changeUser);

					if (!currentRole.Equals(role))
					{
						await _userManager.RemoveFromRolesAsync(changeUser, currentRole);
						await _userManager.AddToRoleAsync(changeUser, role);
					}
				return RedirectToAction("AssignRole");
			}
			

				var users = _dbContext.Users.ToList();
				var roles = _roleManager.Roles.ToList();

				var viewModel = new UserRolesViewModel
				{
					Kullanicilar = users,
					Roller = roles
				};

				return View(viewModel);
			

			
        }
    }
}
