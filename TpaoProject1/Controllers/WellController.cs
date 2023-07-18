using Microsoft.AspNetCore.Mvc;
using TpaoProject1.Data;
using TpaoProject1.Model;

namespace TpaoProject1.Controllers
{
    public class WellController : Controller
    {
        private readonly DatabaseContext _context;
        public WellController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var well = _context.WellTops.ToList();
            return View(well);
        }
        public IActionResult ViewWell(int id)
        {
            var well = _context.WellTops.Find(id);
            
            var formation = _context.Formation.Where(f => f.wellid == id).ToList();

            var all = new WellAndFormation()
            {
                formation = formation,
                well = well
            };
            return View(all);
        }
        public IActionResult AddWell()
        {

            return View();
        }
        [HttpPost]
        [ActionName("AddWell")]
        public IActionResult AddWell(WellTop well /*string Name, string Latitude, string Longitude*/)
        {
            _context.WellTops.Add(well);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddFormation(int id)
        {
            var well = _context.WellTops.Find(id);

            return View(well);
        }
        [HttpPost]
        [ActionName("AddFormation")]
        public IActionResult AddFormation(WellTop well, string Form_type, int Form_meter)
        {
            var id = well.Id;

            Formation formation = new(id)
            {

                Form_meter = Form_meter,
                Form_type = Form_type
            };
            List<Formation> formation_list;
            int? biggest_formation_meter=-1;
            formation_list = _context.Formation.Where(x => x.wellid == id).OrderByDescending(x => x.Form_meter).ToList();
            if(formation_list.Count != 0)
                biggest_formation_meter = formation_list.First().Form_meter;
            var Well = _context.WellTops.Find(id);
            var isExist = _context.Formation.Where(x => x.wellid == id).Where(x => x.Form_type == Form_type).Count();
            
            if (isExist == 0 && Form_meter <= biggest_formation_meter)
            {
				TempData["status"] = "lower_formation";
				return View(Well);
			}
            else if (isExist == 0)
            {
                _context.Formation.Add(formation);
                _context.SaveChanges();
                var Formation = _context.Formation.Where(f => f.wellid == id).ToList();
                var all = new WellAndFormation()
                {
                    formation = Formation,
                    well = Well
                };
                TempData["status"] = "true";
                return View("ViewWell", all);
            }
            else
            {
                TempData["status"] = "same_formation";
                return View(Well);
            }
        }
        public IActionResult UpdateFormation(int id)
        {

            var well = _context.WellTops.Find(id);
            return View(well);
        }
        [HttpPost]
        public IActionResult UpdateFormation(WellTop well) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {

            _context.WellTops.Update(well);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult RemoveWell(int id)
        {
            _context.Remove(_context.WellTops.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
