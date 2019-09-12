using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class WalkinNextOfKinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WalkinNextOfKins
        public ActionResult Index()
        {
            var walkinNextOfKin = db.walkinNextOfKin.Include(w => w.walkingPassenger);
            return View(walkinNextOfKin.ToList());
        }

        // GET: WalkinNextOfKins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkinNextOfKin walkinNextOfKin = db.walkinNextOfKin.Find(id);
            if (walkinNextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(walkinNextOfKin);
        }

        // GET: WalkinNextOfKins/Create
        public ActionResult Create()
        {
            ViewBag.PassengerId = new SelectList(db.walkinPassenger, "PassengerId", "FirstName");
            return View();
        }

        // POST: WalkinNextOfKins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNo,FName,LName,PhoneNumber,Address,PassengerId, Increment")] WalkinNextOfKin walkinNextOfKin)
        {
            var max = db.walkinPassenger.Select(n => n.Increment).Max();
            var passengerid = db.walkinPassenger.Where(n => n.Increment == max).Select(n => n.PassengerId).FirstOrDefault();



            if (ModelState.IsValid)
            {
                walkinNextOfKin.PassengerId = passengerid;
                db.walkinNextOfKin.Add(walkinNextOfKin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PassengerId = new SelectList(db.walkinPassenger, "PassengerId", "FirstName", walkinNextOfKin.PassengerId);
            return View(walkinNextOfKin);
        }

        // GET: WalkinNextOfKins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkinNextOfKin walkinNextOfKin = db.walkinNextOfKin.Find(id);
            if (walkinNextOfKin == null)
            {
                return HttpNotFound();
            }
            ViewBag.PassengerId = new SelectList(db.walkinPassenger, "PassengerId", "FirstName", walkinNextOfKin.PassengerId);
            return View(walkinNextOfKin);
        }

        // POST: WalkinNextOfKins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNo,FName,LName,PhoneNumber,Address,PassengerId")] WalkinNextOfKin walkinNextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(walkinNextOfKin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PassengerId = new SelectList(db.walkinPassenger, "PassengerId", "FirstName", walkinNextOfKin.PassengerId);
            return View(walkinNextOfKin);
        }

        // GET: WalkinNextOfKins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkinNextOfKin walkinNextOfKin = db.walkinNextOfKin.Find(id);
            if (walkinNextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(walkinNextOfKin);
        }

        // POST: WalkinNextOfKins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WalkinNextOfKin walkinNextOfKin = db.walkinNextOfKin.Find(id);
            db.walkinNextOfKin.Remove(walkinNextOfKin);
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
