using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            int? biggest_formation_meter = -1;
            formation_list = _context.Formation.Where(x => x.wellid == id).OrderByDescending(x => x.Form_meter).ToList();
            if (formation_list.Count != 0)
                biggest_formation_meter = formation_list.First().Form_meter;
            var Well = _context.WellTops.Find(id);
            var isExist = _context.Formation.Where(x => x.wellid == id).Where(x => x.Form_type == Form_type).Count();
            if (Form_meter<0 || Form_meter>10000)
            {
                TempData["status"] = "out of order";
                return View(Well);
            }
            else if (isExist == 0 && Form_meter <= biggest_formation_meter)
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
            var formation = _context.Formation.Find(id);
            var well = _context.WellTops.Find(_context.Formation.Find(id).wellid);

            ViewData["name"] = well.Name;
            ViewData["latitude"] = well.Latitude;
            ViewData["longitude"] = well.Longitude;
            TempData["formation_well_id"] = well.Id;

            return View(formation);
        }
        [HttpPost]
        public IActionResult UpdateFormation(Formation formation)
        {
            formation.wellid = Int32.Parse(TempData["formation_well_id"].ToString()); // !!!!!!!!! kullanıcıyı geri yönlendirdiğimizde tempdatadaki veri siliniyor buna çözüm bul !!!!!!!
            var well = _context.WellTops.Find(formation.wellid);
            ViewData["name"] = well.Name;
            ViewData["latitude"] = well.Latitude;
            ViewData["longitude"] = well.Longitude;

            var formation_list = _context.Formation.Where(x => x.wellid == formation.wellid).ToList();
            var index = formation_list.FindIndex(x => x.Id == formation.Id);
            var old_formation = _context.Formation.Find(formation.Id);


            if ((formation_list.Count() - 1 > index) && formation.Form_meter > formation_list[index + 1].Form_meter)
            {
                TempData["Error"] = "bigger";
                return View(old_formation);
            }
            else if (index > 0 && formation.Form_meter < formation_list[index - 1].Form_meter)
            {
                TempData["Error"] = "smaller";
                return View(old_formation);
            }
            else if ((index == 0 && formation_list.Count() > 1 && formation.Form_meter < formation_list[index + 1].Form_meter) || index == 0 && formation_list.Count() == 1 || (index == formation_list.Count() - 1 && formation.Form_meter > formation_list[index - 1].Form_meter) || (formation.Form_meter > formation_list[index - 1].Form_meter && formation.Form_meter < formation_list[index + 1].Form_meter))
            {
                TempData["Error"] = "successful";
                _context.Formation.Update(formation);
                _context.SaveChanges();

                var all = new WellAndFormation()
                {
                    formation = _context.Formation.Where(x => x.wellid == formation.wellid).ToList(),
                    well = _context.WellTops.Find(formation.wellid)

                };
                return View("ViewWell", all);
            }
            else
            {
                return View(old_formation);
            }
        }
        public IActionResult RemoveFormation(int id)
        {
            _context.Remove(_context.Formation.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}