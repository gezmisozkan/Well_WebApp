using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipelines;
using TpaoProject1.Model;
using TpaoProject1.Models;
using TpaoProject1.Services;

namespace TpaoProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private int rnd;
        private int success;

        public HomeController(AppDbContext context)
        {
            _context = context;
            Random random = new Random();
            rnd = random.Next(0, 10);
            ViewData["RandomNumber"] = rnd;

            //if (_context.Game.Any())
            //{
            //    _context.Game.Add(new Game{ NumberOfGames=1, GuessNumber= 2, RememberNumber =5, Success=0, Difference=3});
            //    _context.SaveChanges();

            //}
        }

        public IActionResult Index()
        {
            var games = _context.Game.ToList();
            return View(games);
        }

        [HttpPost]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            var games = _context.Game.ToList();
            int inputNumber = Int16.Parse(HttpContext.Request.Form["inputNumber"]);
            ViewData["InputNumber"] = inputNumber;
            int number = rnd;
            if (inputNumber == number)
            {
                ViewData["Result"] = "You win";
                foreach (var item in games)
                {
                    success = ++item.Success;

                }
            }
            else
            {
                ViewData["Result"] = "You are loser ha ha";
                foreach (var item in games)
                {
                    success = item.Success;

                }
            }

            var guessNumber = inputNumber;
            var rememberNumber = rnd;
            var difference = Math.Abs(inputNumber - rnd);
            Game game = new Game() { GuessNumber = guessNumber, RememberNumber = rememberNumber, Success = success, Difference = difference };

            _context.Add(game);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}