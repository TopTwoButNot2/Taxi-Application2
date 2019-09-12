using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class SeasonsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private SeasonServices ss;
        public SeasonsController()
        {
            this.ss = new SeasonServices();

        }
        // GET: Seasons
        public ActionResult Index()
        {
            return View(ss.GetSeasons());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ss.GetSeason(id) != null)
                return View(ss.GetSeason(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Season season)
        {
            var d = db.Seasons.Where(s=>s.SeasonID==season.SeasonID).Select(m=>m.EndDate).FirstOrDefault();

            if (season.StartDate < season.EndDate && season.StartDate > d)
            {
                ss.AddSeason(season);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "Please double check your dates";
                return View("Create");
            }
            return View(season);
        }
        public ActionResult Edit(int? id)
        {

            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ss.GetSeason(id) != null)
                return View(ss.GetSeason(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Season season)
        {
            if (ModelState.IsValid)
            {
                ss.UpdateSeason(season);
                return RedirectToAction("Index");
            }
            return View(season);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction("Bad_Request", "Error");
            if (ss.GetSeason(id) != null)
                return View(ss.GetSeason(id));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ss.RemoveSeason(ss.GetSeason(id));
            return RedirectToAction("Index");
        }

    }
}