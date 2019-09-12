
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;

namespace TaxiApplication.Controllers
{
    public class ReservedPassengersViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReservedPassengersViewModels
        public ActionResult ReservedPassengersDetails()
        {
            List<ReservedPassengersViewModel> VM = new List<ReservedPassengersViewModel>();

            var list = (from P in db.Passengers
                        join r in db.ReservedPassengers on P.PassengerId equals r.PassengerId
                        join l in db.laggage2 on P.PassengerId equals l.PassengerId

                        select new
                        {

                            P.FirstName,
                            P.LastName,
                            P.EmailAddress,

                            r.TaxiNo,
                            r.RouteName,

                            l.NumberLaggagSmall,
                            l.NumberLaggageMed,
                            l.NumberLaggageLrg,
                            l.TotalPrice




                        }).ToList();

            foreach (var e in list)
            {
                ReservedPassengersViewModel v = new ReservedPassengersViewModel();

                v.FirstName = e.FirstName;
                v.LastName = e.LastName;
                v.EmailAddress = e.EmailAddress;
                v.NumberLaggagSmall = e.NumberLaggagSmall;
                v.TaxiNo = e.TaxiNo;
                v.RouteName = e.RouteName;
                v.NumberLaggageMed = e.NumberLaggageMed;
                v.NumberLaggageLrg = e.NumberLaggageLrg;
                v.TotalPrice = e.TotalPrice;

                //VM.Add(v);
                db.ReservedPassengersViewModels.Add(v);
                db.SaveChanges();

            }
            return View("ReservedPassengersDetails", db.ReservedPassengersViewModels.ToList());

                     
            //return View(db.ReservedPassengersViewModels.ToList());
        }
        public ActionResult ConfirmDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            ReservedPassengersViewModel applicant = db.ReservedPassengersViewModels.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }
        // GET: ReservedPassengersViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedPassengersViewModel reservedPassengersViewModel = db.ReservedPassengersViewModels.Find(id);
            if (reservedPassengersViewModel == null)
            {
                return HttpNotFound();
            }
            return View(reservedPassengersViewModel);
        }

        // GET: ReservedPassengersViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservedPassengersViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VMKY,FirstName,LastName,EmailAddress,TaxiNo,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg,TotalPrice")] ReservedPassengersViewModel reservedPassengersViewModel)
        {
            if (ModelState.IsValid)
            {
                db.ReservedPassengersViewModels.Add(reservedPassengersViewModel);
                db.SaveChanges();
                Session["bookID"] = reservedPassengersViewModel.VMKY;
                return RedirectToAction("ConfirmDetails", new { BookingID = reservedPassengersViewModel.VMKY });
             
            }

            return View(reservedPassengersViewModel);
        }

        public ActionResult PayFast()
        {
            //string email = User.Identity.Name;
            int ID = int.Parse(Session["bookID"].ToString());

            ReservedPassengersViewModel res = new ReservedPassengersViewModel();
            Laggage2 l = new Laggage2();

            res = db.ReservedPassengersViewModels.Find(ID);
            res.TotalPrice = l.TotalPrice;
            try
            {
                // Create the order in your DB and get the ID
                string amount = res.TotalPrice.ToString();
                string orderId = new Random().Next(1, 9999).ToString();
                //string orderId = res.applicationUser.FirstName + "  " + res.applicationUser.LastName; 
                string name = "Taxi Association , Booking Order #" + orderId;
                string description = "Taxi Payment";

                string site = "";
                string merchant_id = "";
                string merchant_key = "";

                // Check if we are using the test or live system
                string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

                if (paymentMode == "test")
                {
                    site = "https://sandbox.payfast.co.za/eng/process?";
                    merchant_id = "10010464";
                    merchant_key = "r8y2nuhvkd3kd";
                }
                else if (paymentMode == "live")
                {
                    site = "https://www.payfast.co.za/eng/process?";
                    merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
                    merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
                }
                else
                {
                    throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
                }

                // Build the query string for payment site

                StringBuilder str = new StringBuilder();
                str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
                str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
                str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
                str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
                str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

                str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
                str.Append("&amount=" + HttpUtility.UrlEncode(amount));
                str.Append("&item_name=" + HttpUtility.UrlEncode(name));
                str.Append("&item_description=" + HttpUtility.UrlEncode(description));

                // Redirect to PayFast
                TempData["payment"] = "Payment for Booking was successful";
                Response.Redirect(site + str.ToString());
                // RedirectToAction("guestDetails", "Reservations1");
            }
            catch (Exception ex)
            {
                // Handle your errors here (log them and tell the user that there was an error)
            }
            return View();
        }
        public ActionResult SPayFast()
        {
            var userId = User.Identity.Name;

            var email = User.Identity.Name;
            //var FindEmail = dc.Users.Where(m => m.UserName == email);
            //var Del = db.DeliveryOptions.Count(m => m.UserName == email);
            //double delivery = 0;
            //if (Del > 0)
            //{
            //    delivery = 100;
            //}
            //string EmailFound = "";
            //foreach (var item in FindEmail)
            //{
            //    EmailFound = item.Email;
            //}
            ReservedPassengersViewModel res = new ReservedPassengersViewModel();
            Laggage2 l = new Laggage2();
            Reservation rn = new Reservation();
            double amount = l.TotalPrice;
            string orderId = new Random().Next(1, 99999).ToString();
            string name = "Blissful Kiddies, Order#" + orderId;
            string description = "Blissful Kiddies";


            string site = "";
            string merchant_id = "";
            string merchant_key = "";

            // Check if we are using the mmor live system
            string paymentMode = System.Configuration.ConfigurationManager.AppSettings["PaymentMode"];

            if (paymentMode == "test")
            {
                site = "https://sandbox.payfast.co.za/eng/process?";
                merchant_id = "10000100";
                merchant_key = "46f0cd694581a";
            }
            else if (paymentMode == "live")
            {
                site = "https://www.payfast.co.za/eng/process?";
                merchant_id = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantID"];
                merchant_key = System.Configuration.ConfigurationManager.AppSettings["PF_MerchantKey"];
            }
            else
            {
                throw new InvalidOperationException("Cannot process payment if PaymentMode (in web.config) value is unknown.");
            }
            // Build the query string for payment site

            StringBuilder str = new StringBuilder();
            str.Append("merchant_id=" + HttpUtility.UrlEncode(merchant_id));
            str.Append("&merchant_key=" + HttpUtility.UrlEncode(merchant_key));
            str.Append("&return_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_ReturnURL"]));
            str.Append("&cancel_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_CancelURL"]));
            //str.Append("&notify_url=" + HttpUtility.UrlEncode(System.Configuration.ConfigurationManager.AppSettings["PF_NotifyURL"]));

            str.Append("&m_payment_id=" + HttpUtility.UrlEncode(orderId));
            str.Append("&amount=" + HttpUtility.UrlEncode(amount.ToString()));
            str.Append("&item_name=" + HttpUtility.UrlEncode(name));
            str.Append("&item_description=" + HttpUtility.UrlEncode(description));

            // Redirect to PayFast
            Response.Redirect(site + str.ToString());

            return View();
        }

        // GET: ReservedPassengersViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedPassengersViewModel reservedPassengersViewModel = db.ReservedPassengersViewModels.Find(id);
            if (reservedPassengersViewModel == null)
            {
                return HttpNotFound();
            }
            return View(reservedPassengersViewModel);
        }

        // POST: ReservedPassengersViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VMKY,FirstName,LastName,EmailAddress,TaxiNo,NumberLaggagSmall,NumberLaggageMed,NumberLaggageLrg,TotalPrice")] ReservedPassengersViewModel reservedPassengersViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservedPassengersViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reservedPassengersViewModel);
        }

        // GET: ReservedPassengersViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedPassengersViewModel reservedPassengersViewModel = db.ReservedPassengersViewModels.Find(id);
            if (reservedPassengersViewModel == null)
            {
                return HttpNotFound();
            }
            return View(reservedPassengersViewModel);
        }

        // POST: ReservedPassengersViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservedPassengersViewModel reservedPassengersViewModel = db.ReservedPassengersViewModels.Find(id);
            db.ReservedPassengersViewModels.Remove(reservedPassengersViewModel);
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
