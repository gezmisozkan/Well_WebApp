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
        public IActionResult Index()
        {
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