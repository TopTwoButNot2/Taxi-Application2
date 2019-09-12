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
    public class PassengerDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PassengerDetails
        public ActionResult Index()
        {
            return View(db.PassengerDetails.ToList());
        }

        // GET: PassengerDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerDetails passengerDetails = db.PassengerDetails.Find(id);
            if (passengerDetails == null)
            {
                return HttpNotFound();
            }
            return View(passengerDetails);
        }

        // GET: PassengerDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult PassengerView()
        {
            List<PassengerDetails> gd = new List<PassengerDetails>();
            var list = (from r in db.Reservations
                        join s in db.Schedules on r.No equals s.No
                        join rm in db.TaxiPosition on s.No equals rm.No
                        join rt in db.TaxiDrivers on rm.td equals rt.td
                        select new
                        {
                            r.DepartureDate,
                            r.NumberLaggagSmall,
                            r.NumberLaggageMed,
                            r.NumberLaggageLrg,
                            rt.driver.FirstName,
                            rt.driver.PhoneNumber,
                            rt.TaxiNo,
                            rt.taxi.TaxiMake.MakeType,
                            rt.taxi.NumSeats,
                            s.RoutePrice,
                            s.route.RouteName
                        }).ToList();
            foreach (var it in list)
            {
                PassengerDetails pd = new PassengerDetails();
                pd.DepartureDate = it.DepartureDate.ToString();
                pd.NumberLaggagSmall = it.NumberLaggagSmall;
                pd.NumberLaggageMed = it.NumberLaggageMed;
                pd.NumberLaggageLrg = it.NumberLaggageLrg;
                pd.DriverName = it.FirstName;
                pd.PhoneNumber = it.PhoneNumber;
                pd.Taxi_RegNo = it.TaxiNo;
                pd.TaxiMake = it.MakeType;
                pd.NumberOfSeats = it.NumSeats;
                pd.RoutePrice = it.RoutePrice;
                pd.RouteName = it.RouteName;
                gd.Add(pd);
            }
            return View("PassengerView", gd);
        }

        // POST: PassengerDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RouteName,DriverName,PhoneNumber,Taxi_RegNo,TaxiMake,NumberOfSeats,RoutePrice,TotalPrice,DepartureDate,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg")] PassengerDetails passengerDetails)
        {
            if (ModelState.IsValid)
            {
                db.PassengerDetails.Add(passengerDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(passengerDetails);
        }

        // GET: PassengerDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerDetails passengerDetails = db.PassengerDetails.Find(id);
            if (passengerDetails == null)
            {
                return HttpNotFound();
            }
            return View(passengerDetails);
        }

        // POST: PassengerDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RouteName,DriverName,PhoneNumber,Taxi_RegNo,TaxiMake,NumberOfSeats,RoutePrice,TotalPrice,DepartureDate,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg")] PassengerDetails passengerDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passengerDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(passengerDetails);
        }

        // GET: PassengerDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerDetails passengerDetails = db.PassengerDetails.Find(id);
            if (passengerDetails == null)
            {
                return HttpNotFound();
            }
            return View(passengerDetails);
        }

        // POST: PassengerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PassengerDetails passengerDetails = db.PassengerDetails.Find(id);
            db.PassengerDetails.Remove(passengerDetails);
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
