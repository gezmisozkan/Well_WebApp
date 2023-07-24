using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TpaoProject1.Data;
using TpaoProject1.Model;
using TpaoProject1.Models;
using Newtonsoft.Json.Linq;


namespace TpaoProject1.Controllers
{
    public class WellController : Controller
    {
        public Dictionary<string, string> Color = new Dictionary<string, string>()
        {
            {"A","#F4A900" },{"B","#E6D690" },{"C","#316650" },{"D","#633A34" },{"E","#D95030" },{"F","#A5A5A5" },
            {"G","#412227" },{"H","#20214F" },{"I","#EC7C26" },{"J","#924E7D" },{"K","#2D572C" },{"L","#EDFF21" },
            {"M","#F5D033" },{"N","#587246" },{"O","#9DA1AA" },{"P","#646B63" },{"Q","#826C34" },{"R","#5E2129" },
            {"S","#2F353B" },{"T","#F4A900" },{"U","#3B3C36" },{"V","#8B8C7A" },{"W","#1C1C1C" },{"X","#6C6960" },
            {"Y","#eb34c6" },{"Z","#541447" }
        };

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
                well = well,
                color = Color
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
        public async Task<IActionResult> AddFormation(WellTop well, string Form_type, int Form_meter)
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
                    well = Well,
                    color = Color
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
            formation.wellid = Int32.Parse(TempData.Peek("formation_well_id").ToString()); // !!!!!!!!! kullanıcıyı geri yönlendirdiğimizde tempdatadaki veri siliniyor buna çözüm bul !!!!!!!
            var well = _context.WellTops.Find(formation.wellid);
            ViewData["name"] = well.Name;
            ViewData["latitude"] = well.Latitude;
            ViewData["longitude"] = well.Longitude;

            var formation_list = _context.Formation.Where(x => x.wellid == formation.wellid).ToList();
            var index = formation_list.FindIndex(x => x.Id == formation.Id);
            var old_formation = _context.Formation.Find(formation.Id);

            if (formation.Form_meter < 0 || formation.Form_meter > 10000)
            {
                TempData["Error"] = "out of order";
                return View(old_formation);
            }
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
                    well = _context.WellTops.Find(formation.wellid),
                    color = Color

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
            var formation = _context.Formation.Find(id);
            _context.Remove(formation);
            _context.SaveChanges();
            var all = new WellAndFormation()
            {
                formation = _context.Formation.Where(x => x.wellid == formation.wellid).ToList(),
                well = _context.WellTops.Find(formation.wellid),
                color = Color
            };
            return View("ViewWell", all);
        }
    }
}
