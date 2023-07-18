using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace TpaoProject1.Controllers
{
	public class AppRolesController : Controller
	{
		
		private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [Authorize]
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
	}
}
