using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;

namespace TaxiApplication.Controllers
{
    public class SchedulesController : Controller
    {
        private ScheduleLogics schedulelogic;
        private RouteServices routeservice;
        private RankLogic ranklogic;
        private ScheduleDateLogics scheduledateLogic;
        private OwnerLogics ownerLogic;
        public SchedulesController()
        {

            this.schedulelogic = new ScheduleLogics();
            this.routeservice = new RouteServices();
            this.ranklogic = new RankLogic();
            this.scheduledateLogic = new ScheduleDateLogics();
            this.ownerLogic = new OwnerLogics();
        }
        // GET: Schedules
        public ActionResult Index()
        {
            return View(schedulelogic.GetSchedules());
        }


        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.ownerID = new SelectList(ownerLogic.GetOwners(), "ownerID", "FirstName");
            ViewBag.RankId = new SelectList(ranklogic.GetRanks(), "RankId", "RankName");
            ViewBag.RouteId = new SelectList(routeservice.GetRoutes(), "RouteId", "RouteName");
            ViewBag.id = new SelectList(scheduledateLogic.GetScheduleDates(), "id", "Count");


            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Schedule schedule)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            BusinessLogic bl = new BusinessLogic();

            var schedul = db.Schedules.Where(x => x.RouteId == schedule.RouteId && x.LoadingTime > schedule.DepertureTime && x.DepertureTime < schedule.LoadingTime);

            if (schedul.Count() != 1)
            {


                schedulelogic.Add(schedule);
                Route r = routeservice.GetRoutes().Find(x => x.RouteId == schedule.RouteId);
                Owner ow = ownerLogic.GetOwners().Find(x => x.ownerID == schedule.ownerID);
                EmailService es = new EmailService();
                es.SendAddScheduleNot(ow.Email, r.RouteName, schedule.Position);

                var owner = ownerLogic.GetOwners().Find(x => x.ownerID == schedule.ownerID);

                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "Try another time";
            }

            //ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName", schedule.ownerID);
            //ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName", schedule.RankId);
            //ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", schedule.RouteId);
            //ViewBag.id = new SelectList(db.ScheduleDates, "id", "Count", schedule.id);


            return View(schedule);
        }

        public ActionResult GetSchedulesById()
        {
            var id = 1;
            if (id == 0)
                return HttpNotFound();
            return new HttpStatusCodeResult(200);
        }


        // GET: Schedules/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Schedule schedule = db.Schedules.Find(id);
        //    if (schedule == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName", schedule.ownerID);
        //    ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName", schedule.RankId);
        //    ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", schedule.RouteId);
        //    ViewBag.id = new SelectList(db.ScheduleDates, "id", "Count", schedule.id);
        //    return View(schedule);
        //}

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "No,DateFrom,DateTo,Position,ownerID,RankId,RouteId")] Schedule schedule)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(schedule).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName", schedule.ownerID);
        //    ViewBag.RankId = new SelectList(db.Ranks, "RankId", "RankName", schedule.RankId);
        //    ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", schedule.RouteId);
        //    return View(schedule);
        //}

        // GET: Schedules/Delete/5
        //        public ActionResult Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Schedule schedule = db.Schedules.Find(id);
        //            if (schedule == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(schedule);
        //        }

        //        // POST: Schedules/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmed(int id)
        //        {
        //            Schedule schedule = db.Schedules.Find(id);
        //            db.Schedules.Remove(schedule);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        protected override void Dispose(bool disposing)
        //        {
        //            if (disposing)
        //            {
        //                db.Dispose();
        //            }
        //            base.Dispose(disposing);
        //        }
    }
}
