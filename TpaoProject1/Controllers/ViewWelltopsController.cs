using Microsoft.AspNetCore.Mvc;

namespace TpaoProject1.Controllers
{
    public class ViewWelltopsController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }
    }
}
