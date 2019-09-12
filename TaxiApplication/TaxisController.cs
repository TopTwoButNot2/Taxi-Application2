using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Models;

namespace TaxiApplication
{
    public class TaxisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Taxis
        public ActionResult Index()
        {
            var taxis = db.Taxis.Include(t => t.owner).Include(t => t.TaxiMake);
            return View(taxis.ToList());
        }

        // GET: Taxis/Details/5
        
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // GET: Taxis/Create
        public ActionResult Create()
        {
            ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName");
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType");
            return View();
        }

        // POST: Taxis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaxiNo,Make,PhoneNumber,ownerID,MakeId")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                db.Taxis.Add(taxi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName", taxi.ownerID);
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType", taxi.MakeId);
            return View(taxi);
        }

        // GET: Taxis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName", taxi.ownerID);
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType", taxi.MakeId);
            return View(taxi);
        }

        // POST: Taxis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaxiNo,Make,PhoneNumber,ownerID,MakeId")] Taxi taxi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ownerID = new SelectList(db.Owners, "ownerID", "FirstName", taxi.ownerID);
            ViewBag.MakeId = new SelectList(db.TaxiMakes, "MakeId", "MakeType", taxi.MakeId);
            return View(taxi);
        }

        // GET: Taxis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Taxi taxi = db.Taxis.Find(id);
            if (taxi == null)
            {
                return HttpNotFound();
            }
            return View(taxi);
        }

        // POST: Taxis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Taxi taxi = db.Taxis.Find(id);
            db.Taxis.Remove(taxi);
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
