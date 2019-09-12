using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{

    public class ScheduleDatesController : Controller
    {
        private ScheduleDateLogics logic;
        public ScheduleDatesController()
        {
            this.logic = new ScheduleDateLogics();
        }
        // GET: ScheduleDates
        public ActionResult Index()
        {

            return View(logic.GetScheduleDates());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ScheduleDate scheduleDate)
        {
            if (ModelState.IsValid)
            {


                logic.AddDate(scheduleDate);
                return RedirectToAction("Index");
            }
            return View(scheduleDate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateManualy(ScheduleDate scheduleDate)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var d = db.ScheduleDates.Select(n => n.DateTo).FirstOrDefault();

            if (scheduleDate.DateFrom > d)
            {
                logic.AddDate2(scheduleDate);
                return RedirectToAction("Index");
            }
            else if (scheduleDate.DateFrom < d)
            {
                TempData["AlertMessage"] = "Please double check your dates";
                return View("CreateManualy");
            }
            return View(scheduleDate);
        }
        public ActionResult CreateManualy()
        {
            return View();
        }
    }
}