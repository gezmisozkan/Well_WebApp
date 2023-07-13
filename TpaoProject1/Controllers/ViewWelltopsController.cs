using Microsoft.AspNetCore.Mvc;
using TpaoProject1.Data;

namespace TpaoProject1.Controllers
{
    public class ViewWelltopsController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }

        public IActionResult AddWellTops()
        {
            return View();
        }


        
      
    }
}
