using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Data;
using TpaoProject1.Model;

namespace TpaoProject1.Controllers
{
    public class ViewWelltopsController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ViewWelltopsController(DatabaseContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> AddWellTop()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWellTop(WellTop model)
        {
            var WellTopList = _dbContext.WellTops.ToList();


            // WellTop well = new WellTop();
            if (!ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                
                model.UserId = user.Id;
                
                var numUserId= model.UserId.ToString();

                var Name = model.Name;
                var Latitude = model.Latitude;
                var Longitude = model.Longitude;
                var WellTopType = model.WellTopType;
                var City = "Ankara";
                var InsertionDate = DateTime.Now;
                var UpdateDate = DateTime.Now;

                WellTop welltop = new WellTop{ UserId = numUserId, Name = Name, Latitude = Latitude, Longitude = Longitude, WellTopType = WellTopType, City = City, InsertionDate = InsertionDate, UpdateDate = UpdateDate };

                _dbContext.WellTops.Add(welltop);
                //_dbContext.SaveChanges();
                await _dbContext.SaveChangesAsync();
               
                
                return RedirectToAction("MainPage", "ViewWelltops");
            }

            return View(WellTopList);
        }


        public async Task<IActionResult> MainPage()
        {


            //var WellTopList= _dbContext.WellTops.ToList();
            var user = await _userManager.GetUserAsync(User);
            var WellTopList = _dbContext.WellTops.Where(w => w.UserId == user.Id).ToList();

            return View(WellTopList);
        }

        
       


        
      
    }
}
