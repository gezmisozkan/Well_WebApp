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
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        private int rnd;
        private int success;
        private int totalGame;

        public HomeController(DatabaseContext context)
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
            return View();
        }


        public IActionResult GameTime()
        {
            var games = _context.Game.ToList();
            foreach (var game in games)
            {
                totalGame++;
                ViewData["totalGame"] = "Total Game : " + totalGame;
                ViewData["illegalSuccess"] = "Total success : " + game.Success;
            }
            return View(games);
        }

        [HttpPost]
        [ActionName("GameTime")]
        public IActionResult GameTimePost()
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
            return RedirectToAction("GameTime");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}