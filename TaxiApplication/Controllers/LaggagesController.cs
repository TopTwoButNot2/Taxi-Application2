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
    public class LaggagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult myBookings()
        {
            var use = db.Passengers.ToList().Find(x => x.EmailAddress == User.Identity.Name);
            var laggagees = db.laggagees.Include(l => l.passengers).Include(l => l.schedule);
            return View(laggagees.ToList().FindAll(x => x.PassengerId == use.PassengerId));
        }
        // GET: Laggages
        public ActionResult Index()
        {
            var laggagees = db.laggagees.Include(l => l.passengers).Include(l => l.schedule);
            return View(laggagees.ToList());
        }
        public ActionResult ConfirmBooking(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage applicant = db.laggagees.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        // GET: Laggages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage laggage = db.laggagees.Find(id);
            if (laggage == null)
            {
                return HttpNotFound();
            }
            return View(laggage);
        }

        // GET: Laggages/Create
        public ActionResult Create()
        {
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName");
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID");
            return View();
        }

        // POST: Laggages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LaggageID,NumberLaggage,picture,Size,status,TotalPrice,PassengerName,DepartureDate,PassengerId,No")] Laggage laggage, HttpPostedFileBase img_upload)
        {
            byte[] data = null;
            data = new byte[img_upload.ContentLength];
            img_upload.InputStream.Read(data, 0, img_upload.ContentLength);
            laggage.picture = data;

            if (ModelState.IsValid)
            {
                laggage.TotalPrice = Convert.ToDouble(laggage.getPrice());
                laggage.Size = laggage.getSize();
                db.laggagees.Add(laggage);
                db.SaveChanges();
                TempData["AlertMessage"] = "Thank you for Booking. Please Confirm your details.";
                //return RedirectToAction("Index");
                return RedirectToAction("ConfirmDetails", "Laggages", new { id = laggage.LaggageID });
            }

            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", laggage.PassengerId);
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID", laggage.No);
            return View(laggage);
        }
        public ActionResult ConfirmDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Laggage applicant = db.laggagees.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        // GET: Laggages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage laggage = db.laggagees.Find(id);
            if (laggage == null)
            {
                return HttpNotFound();
            }
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", laggage.PassengerId);
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID", laggage.No);
            return View(laggage);
        }

        // POST: Laggages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LaggageID,NumberLaggage,picture,Size,status,TotalPrice,PassengerName,DepartureDate,PassengerId,No")] Laggage laggage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laggage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", laggage.PassengerId);
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID", laggage.No);
            return View(laggage);
        }

        // GET: Laggages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laggage laggage = db.laggagees.Find(id);
            if (laggage == null)
            {
                return HttpNotFound();
            }
            return View(laggage);
        }

        // POST: Laggages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laggage laggage = db.laggagees.Find(id);
            db.laggagees.Remove(laggage);
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
