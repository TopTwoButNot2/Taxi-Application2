
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
    public class WalkinDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WalkinDetails
        public ActionResult Index()
        {


            return View(db.WalkinDetails.ToList());
        }

        public ActionResult FullDetails()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            List<WalkinDetails> vm = new List<WalkinDetails>();

            var list = (from wp in db.walkinPassenger
                        join nt in db.walkinNextOfKin on wp.PassengerId equals nt.PassengerId


                        select new
                        {
                            wp.PassengerId,
                            wp.FirstName,
                            wp.TaxiNo,
                            wp.RouteName,
                            wp.StopOver,
                            wp.TotalPrice,
                            wp.DriverName,
                            wp.DepartureDate,
                            
                            nt.LName,
                            nt.PhoneNumber,
                            nt.Address
                        }).ToList();

            foreach(var e in list)
            {
                WalkinDetails v = new WalkinDetails();

                v.PassengerId = e.PassengerId;
                v.FirstName = e.FirstName;
                v.TaxiNo = e.TaxiNo;
                v.RouteName = e.RouteName;
                v.StopOver = e.StopOver;
                v.TotalPrice = e.TotalPrice;
                v.DriverName = e.DriverName;
                v.DepartureDate = e.DepartureDate;
                v.LName = e.LName;
                v.PhoneNumber = e.PhoneNumber;
                v.Address = e.Address;

                vm.Add(v);
            }

            return View("FullDetails", vm);
        }


        // GET: WalkinDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkinDetails walkinDetails = db.WalkinDetails.Find(id);
            if (walkinDetails == null)
            {
                return HttpNotFound();
            }
            return View(walkinDetails);
        }

        // GET: WalkinDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkinDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PassengerId,FirstName,PhoneNumber,TaxiNo,RouteName,StopOver,TotalPrice,DriverName,DepartureDate")] WalkinDetails walkinDetails)
        {
            if (ModelState.IsValid)
            {
                db.WalkinDetails.Add(walkinDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(walkinDetails);
        }

        // GET: WalkinDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkinDetails walkinDetails = db.WalkinDetails.Find(id);
            if (walkinDetails == null)
            {
                return HttpNotFound();
            }
            return View(walkinDetails);
        }

        // POST: WalkinDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PassengerId,FirstName,PhoneNumber,TaxiNo,RouteName,StopOver,TotalPrice,DriverName,DepartureDate")] WalkinDetails walkinDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(walkinDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(walkinDetails);
        }

        // GET: WalkinDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WalkinDetails walkinDetails = db.WalkinDetails.Find(id);
            if (walkinDetails == null)
            {
                return HttpNotFound();
            }
            return View(walkinDetails);
        }

        // POST: WalkinDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            WalkinDetails walkinDetails = db.WalkinDetails.Find(id);
            db.WalkinDetails.Remove(walkinDetails);
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
