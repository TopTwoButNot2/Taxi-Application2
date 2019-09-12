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
    public class NextOfKinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NextOfKins
        public ActionResult Index()
        {
            var nextof = db.NextOfKins;
            var Passengerr = db.Passengers.ToList().Find(x => x.EmailAddress == User.Identity.Name);
            TempData["count"] = nextof.ToList().Where(x => x.PassengerId == Passengerr.PassengerId).Count();
            return View(nextof.ToList().Where(x => x.PassengerId == Passengerr.PassengerId));
        }

        // GET: NextOfKins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // GET: NextOfKins/Create
        public ActionResult Create()
        {
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName");
            return View();
        }

        // POST: NextOfKins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNo,FName,LName,PhoneNumber,Address,PassengerId")] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                var passenger = db.Passengers.ToList().Find(x => x.EmailAddress == User.Identity.Name);
                nextOfKin.PassengerId = passenger.PassengerId;
                db.NextOfKins.Add(nextOfKin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", nextOfKin.PassengerId);
            return View(nextOfKin);
        }

        // GET: NextOfKins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", nextOfKin.PassengerId);
            return View(nextOfKin);
        }

        // POST: NextOfKins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNo,FName,LName,PhoneNumber,Address,PassengerId")] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextOfKin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", nextOfKin.PassengerId);
            return View(nextOfKin);
        }

        // GET: NextOfKins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // POST: NextOfKins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            db.NextOfKins.Remove(nextOfKin);
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
