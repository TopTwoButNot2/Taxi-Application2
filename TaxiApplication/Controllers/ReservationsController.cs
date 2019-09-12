using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class ReservationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reservations
        public ActionResult Index()
        {

            var reservations = db.Reservations.Include(r => r.passengers).Include(r => r.schedule);
            return View(reservations.ToList());
        }

        [HttpGet]
        [Authorize]
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
                PassengerDetails pd= new PassengerDetails();
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
            return View(gd);
        }




        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Availables, "ID", "DriverName");
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName");
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID");
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName");

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg,picture,Size,status,TotalPrice,PassengerName,DepartureDate,PassengerId,ID,No,RouteId")] Reservation reservation, HttpPostedFileBase file)
        {
            
            if (ModelState.IsValid)
            {
                var useriId = User.Identity.GetUserId();
                var userName = User.Identity.GetUserName();



                var season = db.Seasons.Where(n => n.StartDate < DateTime.Now && n.EndDate > DateTime.Now);
                Price s = db.Prices.Where(x=>x.RouteId==reservation.RouteId).ToList().FirstOrDefault();
                if (file != null && file.ContentLength > 0)
                {
                    string newPic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images"), newPic);
                    reservation.picture = newPic;
                    file.SaveAs(path);
                    db.SaveChanges();

                }

                List<LuggagePrice> listprices = db.LaggagePrices.ToList();

                decimal[] prices = { 50, 100, 150 };

                if (reservation.NumberLaggagSmall != null)
                {
                    reservation.TotalPrice += ((decimal)listprices.Find(x=>x.Luggage=="Small").price * (int)reservation.NumberLaggagSmall);
                    Session["sm"] = prices[0] * (int) reservation.NumberLaggagSmall;
                }
                if (reservation.NumberLaggageMed != null)
                {
                    reservation.TotalPrice += ((decimal)listprices.Find(x => x.Luggage == "Medium").price * (int)reservation.NumberLaggageMed);
                    Session["med"] = prices[1] * (int)reservation.NumberLaggageMed;

                }
                if (reservation.NumberLaggageLrg != null)
                {
                    reservation.TotalPrice += ((decimal)listprices.Find(x => x.Luggage == "Large").price * (int)reservation.NumberLaggageLrg);
                    Session["lg"] = prices[2] * (int)reservation.NumberLaggageLrg;

                }
                //reservation.TotalPrice = Convert.ToDouble(reservation.getPrice());
                //reservation.Size = reservation.getSize();
                if (reservation.DepartureDate < DateTime.Now)
                {
                    TempData["AlertMessage"] = "Please double check your date";
                }
                reservation.NumLaggages = Convert.ToInt32(reservation.addLaggages());
                reservation.TotalPrice += s.pricevalue;
                db.Reservations.Add(reservation);
                db.SaveChanges();
                TempData["AlertMessage"] = "Thank you for Booking. Please Confirm your details.";
                //return RedirectToAction("Index");
                return RedirectToAction("ConfirmDetails", new { id = reservation.BookingId });
            }

            ViewBag.ID = new SelectList(db.Availables, "ID", "DriverName", reservation.ID);
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", reservation.PassengerId);
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID", reservation.No);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", reservation.RouteId);

            return View(reservation);
        }
        //// Convert file to binary
        //public byte[] ConvertToBytes(HttpPostedFileBase file)
        //{
        //    BinaryReader reader = new BinaryReader(file.InputStream);
        //    return reader.ReadBytes((int)file.ContentLength);
        //}

        //Display File
        //public FileStreamResult RenderImage(int id)
        //{
        //    MemoryStream ms = null;

        //    var item = db.Reservations.FirstOrDefault(x => x.BookingId == id);
        //    if (item != null)
        //    {
        //        ms = new MemoryStream(item.picture);
        //    }
        //    return new FileStreamResult(ms, item.ImageType);
        //}
        public ActionResult ConfirmDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Reservation applicant = db.Reservations.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Availables, "ID", "DriverName", reservation.ID);
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", reservation.PassengerId);
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID", reservation.No);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", reservation.RouteId);

            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,NumberLaggage,picture,Size,status,TotalPrice,PassengerName,DepartureDate,PassengerId,ID,No")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Availables, "ID", "DriverName", reservation.ID);
            ViewBag.PassengerId = new SelectList(db.Passengers, "PassengerId", "FirstName", reservation.PassengerId);
            ViewBag.No = new SelectList(db.Schedules, "No", "ownerID", reservation.No);
            ViewBag.RouteId = new SelectList(db.Routes, "RouteId", "RouteName", reservation.RouteId);

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
