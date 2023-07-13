using Microsoft.AspNetCore.Mvc;
using TpaoProject1.Data;
using TpaoProject1.Model;
using TpaoProject1.Models;

namespace TpaoProject1.Controllers
{
    public class ViewWelltopsController : Controller
    {
        public IActionResult MainPage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddWellTops()
        {
          

            return View();
        }
        [HttpPost]
        public IActionResult AddWellTops(Welltops welltops)
        {
            var name = welltops.WelltopName;
            var longitude = welltops.Longitude;
            var latitude = welltops.Latitude;
            
            return View();
        }


        
      
    }
}
