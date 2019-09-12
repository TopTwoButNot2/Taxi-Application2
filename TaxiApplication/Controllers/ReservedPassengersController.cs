using Microsoft.AspNet.Identity;
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
    public class ReservedPassengersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReservedPassengers
        public ActionResult Index()
        {
            //var pass = db.Passengers.ToList().Find(x => x.PassengerId == User.Identity.Name);
            var reservedPassengers = db.ReservedPassengers.Include(r => r.Passenger).Include(r => r.TaxiPosition);
            return View(reservedPassengers.ToList());
        }


        public ActionResult DriverIndex()
        {
            var owner = db.Drivers.ToList().Find(x => x.Email == User.Identity.Name);

            var reservedPassengers = db.ReservedPassengers.Include(r => r.Passenger).Include(r => r.TaxiPosition);
            return View(reservedPassengers.ToList().Where(x => x.DriverId == owner.driverID));
        }

        public ActionResult OwnerIndex()
        {


            var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);


            var reservedPassengers = db.ReservedPassengers.Include(r => r.Passenger).Include(r => r.TaxiPosition);
            return View(reservedPassengers.ToList().Where(x => x.OwnerId == owner.ownerID));
        }

        public ActionResult SelectRout(string search)
        {

            var po = db.TaxiPosition.Where(n => n.RouteName == search  ).Select(n => n.Count).FirstOrDefault();
            if (po > 0)
            {
                 po = db.TaxiPosition.Where(n => n.RouteName == search).Select(n => n.Count).Max();

            }
            else
            {
                po = 0;
            }


            var taxiroutes = db.TaxiPosition.Where(x => x.RouteName == search + ", South Africa" ).ToList();
            return View(taxiroutes);
        }

        // GET: ReservedPassengers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedPassengers reservedPassengers = db.ReservedPassengers.Find(id);
            if (reservedPassengers == null)
            {
                return HttpNotFound();
            }
            return View(reservedPassengers);
        }

        // GET: ReservedPassengers/Create
        public ActionResult Create(int? id)
        {
            ViewBag.routename = db.TaxiPosition.Where(t => t.ID == id).FirstOrDefault().RouteName;
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName");
            ViewBag.ID = new SelectList(db.TaxiPosition, "ID", "TaxiNo");

            return View();
        }

        // POST: ReservedPassengers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResPasID,ID,PassengerId")] ReservedPassengers reservedPassengers)
        {
            var pass = db.Passengers.ToList().Find(x => x.EmailAddress == User.Identity.Name);


            var po = db.ReservedPassengers.Where(n => n.ID == reservedPassengers.ID).Select(n => n.ReservedSeats).FirstOrDefault();
            if (po > 0)
            {
                po = db.ReservedPassengers.Where(n => n.ID == reservedPassengers.ID).Select(n => n.ReservedSeats).Max();
            }
            else
            {
                po = 0;
            }



            if (ModelState.IsValid)
            {
                var txNo = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.TaxiNo).FirstOrDefault();

                var rName = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.RouteName).FirstOrDefault();

                var seats = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.NumSeats).FirstOrDefault();

                var rPrice = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.RoutePrice).FirstOrDefault();

                var driverid = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.DriverId).FirstOrDefault();
                var drivername = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.DriverName).FirstOrDefault();

                var ownerid = db.TaxiPosition.Where(x => x.ID == reservedPassengers.ID).Select(x => x.OwnerID).FirstOrDefault();



                reservedPassengers.OwnerId = ownerid;
                reservedPassengers.ReservedSeats = po + 1;
                reservedPassengers.AvailableSeats = reservedPassengers.NumSeats - reservedPassengers.ReservedSeats;
                reservedPassengers.DriverId = driverid;
                reservedPassengers.DtriverName = drivername;
                reservedPassengers.RoutePrice = rPrice;
                reservedPassengers.NumSeats = seats;
                reservedPassengers.ReservedSeats = po + 1;
                reservedPassengers.AvailableSeats = reservedPassengers.NumSeats - reservedPassengers.ReservedSeats;
                reservedPassengers.TaxiNo = txNo;
                reservedPassengers.RouteName = rName;
                reservedPassengers.PassengerId = pass.PassengerId;
                db.ReservedPassengers.Add(reservedPassengers);
                db.SaveChanges();
                return RedirectToAction("Create", "Laggage2");
            }

            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", reservedPassengers.PassengerId);
            ViewBag.ID = new SelectList(db.TaxiPosition, "ID", "TaxiNo", reservedPassengers.ID);
            return View(reservedPassengers);
        }

        // GET: ReservedPassengers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedPassengers reservedPassengers = db.ReservedPassengers.Find(id);
            if (reservedPassengers == null)
            {
                return HttpNotFound();
            }
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", reservedPassengers.PassengerId);
            ViewBag.ID = new SelectList(db.TaxiPosition, "ID", "OwnerID", reservedPassengers.ID);
            return View(reservedPassengers);
        }

        // POST: ReservedPassengers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResPasID,ID,PassengerId")] ReservedPassengers reservedPassengers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservedPassengers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", reservedPassengers.PassengerId);
            ViewBag.ID = new SelectList(db.TaxiPosition, "ID", "OwnerID", reservedPassengers.ID);
            return View(reservedPassengers);
        }

        // GET: ReservedPassengers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedPassengers reservedPassengers = db.ReservedPassengers.Find(id);
            if (reservedPassengers == null)
            {
                return HttpNotFound();
            }
            return View(reservedPassengers);
        }

        // POST: ReservedPassengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservedPassengers reservedPassengers = db.ReservedPassengers.Find(id);
            db.ReservedPassengers.Remove(reservedPassengers);
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
