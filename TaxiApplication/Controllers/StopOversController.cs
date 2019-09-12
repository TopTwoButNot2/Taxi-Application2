using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;
using TaxiApplication.BusinessLogics;
using TaxiApplication.Data;


namespace TaxiApplication.Controllers
{
    public class StopOversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StopOvers
        public ActionResult Index()
        {
            TempData["count"] = db.StopOvers.Count();
            var stopOvers = db.StopOvers.Include(s => s.Route);
            return View(stopOvers.ToList());
        }

        // GET: StopOvers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopOver stopOver = db.StopOvers.Find(id);
            if (stopOver == null)
            {
                return HttpNotFound();
            }
            return View(stopOver);
        }

        // GET: StopOvers/Create
        public ActionResult Create()
        {
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName");
            return View();
        }

        // POST: StopOvers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StopID,lat,longitude,stop,RouteId")] StopOver stopOver)
        {
            var stopOvers = db.StopOvers.Where(x => x.stop == stopOver.stop);
            if (stopOvers.Count() != 0)
            {
                ModelState.AddModelError("", "The Stop-Overs already exists");
            }
            if (ModelState.IsValid)
            {
                var name = stopOver.stop.Split(';');
                foreach (var n in name)
                {
                    stopOver.stop = n;
                    db.StopOvers.Add(stopOver);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", stopOver.RouteId);
            return View(stopOver);
        }

        // GET: StopOvers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopOver stopOver = db.StopOvers.Find(id);
            if (stopOver == null)
            {
                return HttpNotFound();
            }
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", stopOver.RouteId);
            return View(stopOver);
        }

        // POST: StopOvers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StopID,lat,longitude,stop,RouteId")] StopOver stopOver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stopOver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", stopOver.RouteId);
            return View(stopOver);
        }

        // GET: StopOvers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StopOver stopOver = db.StopOvers.Find(id);
            if (stopOver == null)
            {
                return HttpNotFound();
            }
            return View(stopOver);
        }

        // POST: StopOvers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StopOver stopOver = db.StopOvers.Find(id);
            db.StopOvers.Remove(stopOver);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
