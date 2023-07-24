using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipelines;
using TpaoProject1.Data;
using TpaoProject1.Model;
using TpaoProject1.Models;

namespace TpaoProject1.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var tespit = _context.WellTops.Where(p => p.WellTopType == "tespit").Count();
			var uretim = _context.WellTops.Where(p => p.WellTopType == "üretim").Count();
			var arama = _context.WellTops.Where(p => p.WellTopType == "arama").Count();
			var toplam = tespit + uretim + arama;
			ViewBag.uretim = uretim;
			ViewBag.arama = arama;
			ViewBag.tespit = tespit;
			ViewBag.toplam = toplam;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(WellTop model)
		{

			return View();
		}
		public IActionResult History() {
            return View();
        }
        public IActionResult History() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}