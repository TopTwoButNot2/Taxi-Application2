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
    public class ReservedViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReservedViewModels
        public ActionResult Index()
        {
            return View(db.ReservedViewModels.ToList());
        }
        public ActionResult Confirmation()
        {
            List<ReservedViewModel> vm = new List<ReservedViewModel>();

            var list = (from p in db.Passengers
                        join r in db.reservedList on p.PassengerId equals r.PassengerId
                        join l in db.laggage2 on p.PassengerId equals l.PassengerId

                        select new
                        {
                            r.RouteName,
                            r.RoutePrice,
                            r.DriverName,
                            r.TaxiNo,
                            r.OwnerTel,
                            r.DriverTel,
                            r.TaxiMake,
                            r.TaxiModel,
                            r.Image,
                            r.PassengerId,
                            r.PassengerName,
                            r.PassengerTel,
                               
                            l.NumberLaggageLrg,
                            l.NumberLaggageMed,
                            l.NumberLaggagSmall,
                            l.TotalPrice,
                            l.DepartureDate,

                               }).ToList();
            foreach(var x in list)
            {
                ReservedViewModel v = new ReservedViewModel();

                v.RouteName = x.RouteName;
                v.RoutePrice = x.RoutePrice;
                v.DriverName = x.DriverName;
                v.DriverTel = x.DriverTel;
                v.TaxiNo = x.TaxiNo;
                v.OwnerTel = x.OwnerTel;
                v.TaxiMake = x.TaxiMake;
                v.TaxiModel = x.TaxiModel;
                v.Image = x.Image;
                v.PassengerId = x.PassengerId;
                v.PassengerName = x.PassengerName;
                v.PassengerTel = x.PassengerTel;

                v.NumberLaggageLrg = x.NumberLaggageLrg;
                v.NumberLaggageMed = x.NumberLaggageMed;
                v.NumberLaggagSmall = x.NumberLaggagSmall;
                v.LaggagePrice = x.TotalPrice;

                vm.Add(v);
            }

            return View("Confirmation", vm);
        }

        // GET: ReservedViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedViewModel reservedViewModel = db.ReservedViewModels.Find(id);
            if (reservedViewModel == null)
            {
                return HttpNotFound();
            }
            return View(reservedViewModel);
        }

        // GET: ReservedViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservedViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VMkey,RouteName,RoutePrice,DriverName,TaxiNo,OwnerTel,DriverTel,TaxiMake,TaxiModel,Image,LoadingTime,DepertureTime,PassengerId,PassengerName,PassengerTel,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg,LaggagePrice,DepartureDate")] ReservedViewModel reservedViewModel)
        {
            if (ModelState.IsValid)
            {
                db.ReservedViewModels.Add(reservedViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reservedViewModel);
        }

        // GET: ReservedViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedViewModel reservedViewModel = db.ReservedViewModels.Find(id);
            if (reservedViewModel == null)
            {
                return HttpNotFound();
            }
            return View(reservedViewModel);
        }

        // POST: ReservedViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VMkey,RouteName,RoutePrice,DriverName,TaxiNo,OwnerTel,DriverTel,TaxiMake,TaxiModel,Image,LoadingTime,DepertureTime,PassengerId,PassengerName,PassengerTel,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg,LaggagePrice,DepartureDate")] ReservedViewModel reservedViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservedViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservedViewModel);
        }

        // GET: ReservedViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedViewModel reservedViewModel = db.ReservedViewModels.Find(id);
            if (reservedViewModel == null)
            {
                return HttpNotFound();
            }
            return View(reservedViewModel);
        }

        // POST: ReservedViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservedList reservedList = db.reservedList.Find(id);

            db.reservedList.Remove(reservedList);

            db.SaveChanges();
            return RedirectToAction("Confirmation");
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
