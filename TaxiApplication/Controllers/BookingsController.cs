
using Microsoft.AspNet.Identity;
using PayFast;
using PayFast.AspNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaxiApplication.Data;
using TaxiApplication.Models;

namespace TaxiApplication.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //--------------------------------------------------------------------------------------------
        public ActionResult CancelBooking(int? id)
        {
            Booking booking = db.Bookings.Find(id);
            booking.status = "Cancelled";
            db.Entry(booking).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ConfirmBooking");
        }
        public ActionResult ConfirmCollection(int? id)
        {
            Booking booking = db.Bookings.Find(id);
            booking.status = "Approved";
            TaxiHire v = db.TaxiHires.ToList().Find(x => x.vehicleID == booking.vehicleID);
            v.Status = "Not Available";
            db.Entry(booking).State = EntityState.Modified;
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AvailableCar(int? id)
        {
            Booking booking = db.Bookings.Find(id);
            
            booking.status = "Complete";
            booking.Booking_Cost = booking.TotalCost;
            booking.FinalTotalCost = booking.calcFinalPrice();
            TaxiHire v = db.TaxiHires.ToList().Find(x => x.vehicleID == booking.vehicleID);
            v.Status = "Available";
            v.CurrentMilage = booking.MilageIn;
            db.Entry(booking).State = EntityState.Modified;
            db.Entry(v).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Bookings");
        }
        //--------------------------------------------------------------------------------------------

        // GET: Bookings
        //[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var use = db.Users.ToList().Find(x => x.Email == User.Identity.Name);
            var bookings = db.Bookings.Include(b => b.taxiH);
            //return View(bookings.ToList().FindAll(x => x.taxiH.ownerID == use.Id));
            return View(bookings.ToList());
        }
        //[Authorize(Roles = "Client")]
        public ActionResult myBookings()
        {
            var use = db.Passengers.ToList().Find(x => x.EmailAddress == User.Identity.Name);
            var bookings = db.Bookings.Include(b => b.taxiH);
            return View(bookings.ToList().FindAll(x => x.userId == use.EmailAddress));
        }
        public ActionResult OwnerBookings()
        {
            var use = db.Users.ToList().Find(x => x.Email == User.Identity.Name);
            var bookings = db.Bookings.Include(b => b.taxiH);
            return View(bookings.ToList().FindAll(x => x.taxiH.ownerID == use.Id));
        }
        // GET: Bookings/Details/5
        public ActionResult ConfirmBooking(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }
        public ActionResult ReturnVehicle(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking.status == "Pending")
            {
                TempData["AlertMessage"] = "Cannot return a car that has not been taken";
            }
            booking.UsedKM = booking.calcUsedKM();
            booking.TotalCost = booking.calcKMCOst();
                ViewBag.pick = booking.PickUpDate.Date;
                ViewBag.retur = booking.ReturnDate.Date;
                ViewBag.userID = booking.userId;
                ViewBag.status = booking.status;
                ViewBag.bookID = booking.BookingId;
        
            
            
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.vehicleID = new SelectList(db.TaxiHires, "vehicleID", "vehicleID", booking.vehicleID);
            return View(booking);
            //return RedirectToAction("Pay","Payments", new {BookingId=booking.BookingId });
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        //[Authorize(Roles = "Client")]
        public async Task<ActionResult> Create(int id)
        {
            ViewBag.Id = id;
            ViewBag.vehicleID = new SelectList(db.TaxiHires, "vehicleID", "Status");
            ViewBag.extrasID = new SelectList(await db.Extras.ToListAsync(), "extrasName", "extrasName");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Client")]
        public async Task<ActionResult> Create([Bind(Include = "BookingId,userId,FinalTotalCost,BasicPrice,PickUpDate,ReturnDate,Booking_Cost,status,TowBar,NavGPS,ChildSeat,BicycleRack,MobileWiFi,vehicleID,MilageIn,RouteName,RouteDistance")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserName();
                booking.userId =user;
                //Client cl = db.Clients.ToList().Find(x => x.userId == us.Id);
                //booking.ClientId = cl.ClientId;
                if(booking.PickUpDate > booking.ReturnDate)
                {
                    TempData["AlertMessage"] = "Pick up date can't come after return date";
                }
                else
                {
                    booking.seats = booking.getNumOfSeats();
                    booking.numOfDays = calcNum_of_Days(booking.ReturnDate, booking.PickUpDate);
                    booking.Booking_Cost = booking.caclOverNightFe();
                    booking.DayPrice = booking.GetBasicCost();
                    booking.deposite = booking.calcDeposit();
                    booking.kiloCost = booking.caclFreeKM();
                    booking.MilageOut = booking.getCurrentMilage();
                    //booking.ExtraPrice = booking.calcExtra();
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "Thank you. Almost done. Please Confirm your details.";
                    return RedirectToAction("ConfirmDetails", "Bookings", new { id = booking.BookingId });
                }                         

            }

            ViewBag.vehicleID = new SelectList(db.TaxiHires, "vehicleID", "Status", booking.vehicleID);
            ViewBag.extrasID = new SelectList(db.Extras, "extrasName", "extrasName");
            //ListExtra listEx = new ListExtra();
            //listEx.Extra = ViewBag.extrasID;
            return View(booking);
        }
        public double calcNum_of_Days(DateTime returnDate, DateTime pickUp)
        {
           
            TimeSpan difference = returnDate.Subtract(pickUp);
            var Days = difference.TotalDays;
            return Days;

        }
        public ActionResult ConfirmDetails(int? id)
        {
            TempData["AlertMessage"] = "Thank you for booking, and payment is successfull";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Booking applicant = db.Bookings.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.vehicleID = new SelectList(db.TaxiHires, "vehicleID", "Status", booking.vehicleID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,userId,PickUpDate,ReturnDate,Booking_Cost,status,MilageIn,MilageOut,vehicleID,RouteName,RouteDistance")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.vehicleID = new SelectList(db.TaxiHires, "vehicleID", "Status", booking.vehicleID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
        //Payment
        #region Fields

        private readonly PayFastSettings payFastSettings;

        #endregion Fields

        #region Constructor

        public BookingsController()
        {
            this.payFastSettings = new PayFastSettings();
            this.payFastSettings.MerchantId = ConfigurationManager.AppSettings["MerchantId"];
            this.payFastSettings.MerchantKey = ConfigurationManager.AppSettings["MerchantKey"];
            this.payFastSettings.PassPhrase = ConfigurationManager.AppSettings["PassPhrase"];
            this.payFastSettings.ProcessUrl = ConfigurationManager.AppSettings["ProcessUrl"];
            this.payFastSettings.ValidateUrl = ConfigurationManager.AppSettings["ValidateUrl"];
            this.payFastSettings.ReturnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            this.payFastSettings.CancelUrl = ConfigurationManager.AppSettings["CancelUrl"];
            this.payFastSettings.NotifyUrl = ConfigurationManager.AppSettings["NotifyUrl"];
        }

        #endregion Constructor

        #region Methods



        public ActionResult Recurring()
        {
            var recurringRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            recurringRequest.merchant_id = this.payFastSettings.MerchantId;
            recurringRequest.merchant_key = this.payFastSettings.MerchantKey;
            recurringRequest.return_url = this.payFastSettings.ReturnUrl;
            recurringRequest.cancel_url = this.payFastSettings.CancelUrl;
            recurringRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            recurringRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            recurringRequest.m_payment_id = "8d00bf49-e979-4004-228c-08d452b86380";
            recurringRequest.amount = 20;
            recurringRequest.item_name = "Recurring Option";
            recurringRequest.item_description = "Some details about the recurring option";

            // Transaction Options
            recurringRequest.email_confirmation = true;
            recurringRequest.confirmation_address = "drnendwandwe@gmail.com";

            // Recurring Billing Details
            recurringRequest.subscription_type = SubscriptionType.Subscription;
            recurringRequest.billing_date = DateTime.Now;
            recurringRequest.recurring_amount = 20;
            recurringRequest.frequency = BillingFrequency.Monthly;
            recurringRequest.cycles = 0;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{recurringRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public ActionResult OnceOff()
        {
            var onceOffRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            onceOffRequest.merchant_id = this.payFastSettings.MerchantId;
            onceOffRequest.merchant_key = this.payFastSettings.MerchantKey;
            onceOffRequest.return_url = this.payFastSettings.ReturnUrl;
            onceOffRequest.cancel_url = this.payFastSettings.CancelUrl;
            onceOffRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            
            onceOffRequest.email_address = "sbtu01@payfast.co.za";
            double amount =db.Bookings.Select(p=>p.deposite).FirstOrDefault();
            var products = db.Bookings.Select(x => x.userId).ToList();
            // Transaction Details
            onceOffRequest.m_payment_id = "";
            onceOffRequest.amount = amount;
            onceOffRequest.item_name = "Once off option";
            onceOffRequest.item_description = "Some details about the once off payment";


            // Transaction Options
            onceOffRequest.email_confirmation = true;
            onceOffRequest.confirmation_address = "sbtu01@payfast.co.za";

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{onceOffRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public ActionResult AdHoc()
        {
            var adHocRequest = new PayFastRequest(this.payFastSettings.PassPhrase);

            // Merchant Details
            adHocRequest.merchant_id = this.payFastSettings.MerchantId;
            adHocRequest.merchant_key = this.payFastSettings.MerchantKey;
            adHocRequest.return_url = this.payFastSettings.ReturnUrl;
            adHocRequest.cancel_url = this.payFastSettings.CancelUrl;
            adHocRequest.notify_url = this.payFastSettings.NotifyUrl;

            // Buyer Details
            adHocRequest.email_address = "sbtu01@payfast.co.za";

            // Transaction Details
            adHocRequest.m_payment_id = "";
            adHocRequest.amount = 70;
            adHocRequest.item_name = "Adhoc Agreement";
            adHocRequest.item_description = "Some details about the adhoc agreement";

            // Transaction Options
            adHocRequest.email_confirmation = true;
            adHocRequest.confirmation_address = "sbtu01@payfast.co.za";

            // Recurring Billing Details
            adHocRequest.subscription_type = SubscriptionType.AdHoc;

            var redirectUrl = $"{this.payFastSettings.ProcessUrl}{adHocRequest.ToString()}";

            return Redirect(redirectUrl);
        }

        public ActionResult Return()
        {
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Notify([ModelBinder(typeof(PayFastNotifyModelBinder))]PayFastNotify payFastNotifyViewModel)
        {
            payFastNotifyViewModel.SetPassPhrase(this.payFastSettings.PassPhrase);

            var calculatedSignature = payFastNotifyViewModel.GetCalculatedSignature();

            var isValid = payFastNotifyViewModel.signature == calculatedSignature;

            System.Diagnostics.Debug.WriteLine($"Signature Validation Result: {isValid}");

            // The PayFast Validator is still under developement
            // Its not recommended to rely on this for production use cases
            var payfastValidator = new PayFastValidator(this.payFastSettings, payFastNotifyViewModel, IPAddress.Parse(this.HttpContext.Request.UserHostAddress));

            var merchantIdValidationResult = payfastValidator.ValidateMerchantId();

            System.Diagnostics.Debug.WriteLine($"Merchant Id Validation Result: {merchantIdValidationResult}");

            var ipAddressValidationResult = payfastValidator.ValidateSourceIp();

            System.Diagnostics.Debug.WriteLine($"Ip Address Validation Result: {merchantIdValidationResult}");

            // Currently seems that the data validation only works for successful payments
            if (payFastNotifyViewModel.payment_status == PayFastStatics.CompletePaymentConfirmation)
            {
                var dataValidationResult = await payfastValidator.ValidateData();

                System.Diagnostics.Debug.WriteLine($"Data Validation Result: {dataValidationResult}");
            }

            if (payFastNotifyViewModel.payment_status == PayFastStatics.CancelledPaymentConfirmation)
            {
                System.Diagnostics.Debug.WriteLine($"Subscription was cancelled");
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Error()
        {
            return View();
        }

        #endregion Methods
    }
}
