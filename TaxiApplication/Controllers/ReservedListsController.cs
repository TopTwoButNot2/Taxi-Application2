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
    public class ReservedListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReservedLists
        public ActionResult Index()
        {
            var usee = db.Users.ToList().Find(x => x.Email == User.Identity.Name);
            return View(db.reservedList.ToList().Where(p => p.PassengerId == usee.Email));
        }

        public ActionResult ManagerIndex()
        {

            var manager = db.RankManagers.ToList().Find(x => x.Email == User.Identity.Name);

            var rout = db.Routes.Where(x => x.rankmanagerID == manager.rankmanagerID).Select(x => x.RouteName).FirstOrDefault();

            return View(db.reservedList.Where(x=>x.RouteName==rout).ToList());
        }
        public ActionResult DriverIndex()
        {

            var driver = db.Drivers.ToList().Find(x => x.Email == User.Identity.Name);

            var taxidriver = db.TaxiDrivers.Where(x => x.driverID == driver.driverID).Select(x => x.TaxiNo).FirstOrDefault();

            return View(db.reservedList.Where(x => x.TaxiNo == taxidriver).ToList());
        }
        //public ActionResult RankManagerIndex()
        //{
        //    var manager = db.RankManagers.ToList().Find(x => x.Email == User.Identity.Name);


        //    return View(db.reservedList.Where(x=>x.RouteName==).ToList());
        //}
        public ActionResult ViewSeats(string search)
        {

            var po = db.reservedList.Where(x => x.TaxiNo == search).Select(x => x.ReservedPassengers).FirstOrDefault();
            if (po > 0)
            {
                po = db.reservedList.Where(x => x.TaxiNo == search).Select(x => x.ReservedPassengers).Max();
            }
            else
            {
                po = 0;
            }




            return View(db.reservedList.Where(x=>x.TaxiNo==search && x.ReservedPassengers==po ).ToList());
        }

        // GET: ReservedLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedList reservedList = db.reservedList.Find(id);
            if (reservedList == null)
            {
                return HttpNotFound();
            }
            return View(reservedList);
        }

        // GET: ReservedLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservedLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Key,td,OwnerID,RouteName,RoutePrice,DriverName,NumSeats,TaxiNo,LoadingTime,DepertureTime")] ReservedList reservedList)
        {
            if (ModelState.IsValid)
            {
                db.reservedList.Add(reservedList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservedList);
        }

        // GET: ReservedLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedList reservedList = db.reservedList.Find(id);
            if (reservedList == null)
            {
                return HttpNotFound();
            }
            return View(reservedList);
        }

        // POST: ReservedLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Key,td,OwnerID,RouteName,RoutePrice,DriverName,NumSeats,TaxiNo,LoadingTime,DepertureTime")] ReservedList reservedList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservedList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservedList);
        }

        // GET: ReservedLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedList reservedList = db.reservedList.Find(id);
            if (reservedList == null)
            {
                return HttpNotFound();
            }
            return View(reservedList);
        }

        // POST: ReservedLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservedList reservedList = db.reservedList.Find(id);
            db.reservedList.Remove(reservedList);
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
