using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using TpaoProject1.Areas.Identity.Data;
using TpaoProject1.Data;
using TpaoProject1.Model;

namespace TpaoProject1.Controllers
{
    public class ViewWelltopsController : BaseController
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
			ViewBag.ActionName = "AddWellTop";
			ViewBag.ButtonText = "Kaydet";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWellTop(WellTop model)
        {
			ViewBag.ActionName = "AddWellTop";
			ViewBag.ButtonText = "Kaydet";
			var WellTopList = _dbContext.WellTops.ToList();

			
			// WellTop well = new WellTop();
			if (ModelState.IsValid)
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
                var name = welltop.Name;
                var longitude = welltop.Longitude;
                var latitude = welltop.Latitude;

				if (!IsNameExists(name) && !IsLocationExists(longitude, latitude))
				{
                    _dbContext.WellTops.Add(welltop);
                }
                else
                {
                    BasicNotification("Tekrar Deneyiniz...", NotificationType.Error, "Kuyu Eklenemedi");
                    return View();
				}
                //_dbContext.SaveChanges();
                BasicNotification("Anasayfaya yönlendiriliyorsunuz...", NotificationType.Success, "Kuyu Başarıyla Eklendi!");
                await _dbContext.SaveChangesAsync();

                
                return RedirectToAction("MainPage", "ViewWelltops");
            }
          

            return View();
        }
		public bool IsNameExists(string name)
		{
			return _dbContext.WellTops.Any(u => u.Name == name);
		}

		public bool IsLocationExists(string longitude, string latitude)
		{
			return _dbContext.WellTops.Any(u => u.Latitude == latitude) && _dbContext.WellTops.Any(u => u.Longitude == longitude);
		}

		public async Task<IActionResult> MainPage()
        {


            var user = await _userManager.GetUserAsync(User);
            var WellTopList = _dbContext.WellTops.ToList();


            if (User.IsInRole("Admin"))
            {
                WellTopList = _dbContext.WellTops.ToList();
            }
            else
            {
                WellTopList = _dbContext.WellTops.Where(w => w.UserId == user.Id).ToList();
            }


            var users = _dbContext.Users.ToList();


            var viewModel = new UserRolesViewModel
            {
                Kullanicilar = users,
                Kuyular = WellTopList


            };


            return View(viewModel);
        }

        
       public IActionResult Delete(int id)
        {
            DeleteNotification("Silinen Kuyu Geri Getirilmez!", NotificationType.Warning, "Emin Misiniz?");

			ViewBag.ActionName = "Delete";
			ViewBag.ButtonText = "Sil";

			var kuyu = _dbContext.WellTops.Find(id);
            _dbContext.Remove(kuyu);
            _dbContext.SaveChanges();
			return RedirectToAction("MainPage", "ViewWelltops");
		}
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.ActionName = "Update";
            ViewBag.ButtonText = "Güncelle";
			var kuyu = _dbContext.WellTops.Find(id);
            var updateUserId = kuyu.UserId;
            TempData["userid"] = updateUserId;
			return View(kuyu);
		}
        [HttpPost]
		public async Task<IActionResult> Update(WellTop welltop)
        {
            welltop.UserId = TempData["userid"].ToString();
            _dbContext.WellTops.Update(welltop);
			_dbContext.SaveChangesAsync();
			return RedirectToAction("MainPage", "ViewWelltops");

		}



	}
}
