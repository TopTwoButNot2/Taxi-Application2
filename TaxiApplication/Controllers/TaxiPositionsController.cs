using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TaxiApplication.Data;
using TaxiApplication.BusinessLogics;
using System.Data.Entity;
using PayFast;
using System.Net;
using PayFast.AspNet;
using System.Threading.Tasks;
using System.Configuration;

namespace TaxiApplication.Controllers
{
    public class TaxiPositionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxiPositionLogics tpLogic;
        private TaxiDriverLogics tdLogic;
        private ScheduleLogics sLogic;
        public TaxiPositionsController()
        {
            this.tpLogic = new TaxiPositionLogics();
            this.tdLogic = new TaxiDriverLogics();
            this.sLogic = new ScheduleLogics();

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
        // GET: TaxiPositions
        public ActionResult Index()
        {
            return View(tpLogic.GetOwnDetails(User.Identity.Name));
        }
       
        public ActionResult ManagerIndex()
        {

            var manager = db.RankManagers.ToList().Find(x => x.Email == User.Identity.Name);

            var rout = db.Routes.Where(x => x.rankmanagerID == manager.rankmanagerID).Select(x => x.RouteName).FirstOrDefault();

            return View(db.TaxiPosition.Where(x => x.RouteName == rout).ToList());
        }


        public ActionResult ListOfTaxisForPassenger(string search)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return View(db.TaxiPosition.Include(zz=>zz.schedule).Include(zz=>zz.TaxiDriver).Where(xx=>xx.RouteName==search + ", South Africa" || search==null).ToList());
        }

        public ActionResult GroupedRoute()
        {
            return View(tpLogic.GetTaxipositionList());
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GroupedRoute(TaxiPosition taxiPosition)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        ReservedList reee = new ReservedList();

 
        //        reee.RouteName = taxiPosition.RouteName;
        //        reee.RoutePrice = taxiPosition.RoutePrice;
        //        reee.DriverName = taxiPosition.DriverName;
        //        reee.NumSeats = taxiPosition.NumSeats;
        //        reee.TaxiNo = taxiPosition.TaxiNo;
        //        reee.LoadingTime = taxiPosition.LoadingTime;
        //        reee.DepertureTime = taxiPosition.DepertureTime;
            

        //        tpLogic.Avail(taxiPosition);
        //        RedirectToAction("Index", "ReservedList");
        //    }
           

        //    return View(taxiPosition);
        //}

        public ActionResult Create2()
        {
            ViewBag.No = new SelectList(sLogic.GetSchedules(), "No", "Position");
            // ViewBag.td = new SelectList(tdLogic.GetTaxiDrivers(), "td", "TaxiNo");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(TaxiPosition taxiPosition)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                ReservedList reee = new ReservedList();
                var num = db.TaxiPosition.ToList().Find(c=>c.NumSeats==taxiPosition.NumSeats);

          
                reee.RouteName = taxiPosition.RouteName;
                reee.RoutePrice = taxiPosition.RoutePrice;
                reee.DriverName = taxiPosition.DriverName;
                //reee.NumSeats = num;
                reee.TaxiNo = taxiPosition.TaxiNo;
                reee.LoadingTime = taxiPosition.LoadingTime;
                reee.DepertureTime = taxiPosition.DepertureTime;
             

                db.reservedList.Add(reee);
                db.SaveChanges();
                return RedirectToAction("Index", "ReservedLists");
            }
            return View(taxiPosition);
        }
    
        public ActionResult Index2()
        {
            return View(tpLogic.GetOwnDetails(User.Identity.Name));
        }
        public ActionResult Create()
        {
            ViewBag.No = new SelectList(sLogic.GetSchedules(), "No", "Position");
            // ViewBag.td = new SelectList(tdLogic.GetTaxiDrivers(), "td", "TaxiNo");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaxiPosition taxiPosition)
        {
            var count = db.TaxiPosition.Where(x => x.No == taxiPosition.No).Select(x => x.Count).FirstOrDefault();
            if (count > 0)
            {
                count = db.TaxiPosition.Where(x => x.No == taxiPosition.No).Select(x => x.Count).Max();
            }
            else
            {
                count = 0;
            }

          

            if (ModelState.IsValid)
            {
                var taxipositionTD = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.TaxiNo).FirstOrDefault();
                var owner = db.Owners.ToList().Find(x => x.Email == User.Identity.Name);

                var Start = db.Schedules.Where(x => x.No == taxiPosition.No).Select(x => x.LoadingTime).FirstOrDefault();
                var end = db.Schedules.Where(x => x.No == taxiPosition.No).Select(x => x.DepertureTime).FirstOrDefault();
                var routee = db.Schedules.Where(x => x.No == taxiPosition.No).Select(x => x.Routeee).FirstOrDefault();
                var seats = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.NumSeats).FirstOrDefault();
                var driverid = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.driverID).FirstOrDefault();
                var drivername = db.Drivers.Where(x => x.driverID == driverid).Select(x => x.FirstName).FirstOrDefault();
                var routeprice = db.Schedules.Where(x => x.No == taxiPosition.No).Select(x => x.RoutePrice).FirstOrDefault();
                var ownerid = db.Schedules.Where(x => x.No == taxiPosition.No).Select(x => x.ownerID).FirstOrDefault();


                var driverTel = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.DriverTel).FirstOrDefault();

                var taxiModeltype = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.TaxiModel).FirstOrDefault();

                var taxiMake = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.TaxiMake).FirstOrDefault();

                var image = db.TaxiDrivers.Where(x => x.td == taxiPosition.td).Select(x => x.Image).FirstOrDefault();







                 taxiPosition.DriverTel = driverTel;
                 taxiPosition.TaxiMake = taxiMake;
                 taxiPosition.TaxiModel = taxiModeltype;
                taxiPosition.OwnerTel = owner.PhoneNumber;
                taxiPosition.OwnerID = ownerid;
                taxiPosition.Image = image;
                taxiPosition.Count = count + 1;
                taxiPosition.DriverId = driverid;
                taxiPosition.RoutePrice = routeprice;
                taxiPosition.DriverName = drivername;
                taxiPosition.NumSeats = seats;
                taxiPosition.RouteName = routee;
                taxiPosition.LoadingTime =Convert.ToString( Start);
                taxiPosition.DepertureTime = Convert.ToString(end);
                taxiPosition.TaxiNo = taxipositionTD;
                taxiPosition.OwnerID = owner.ownerID;
                db.TaxiPosition.Add(taxiPosition);
                db.SaveChanges();
                //tpLogic.Add(taxiPosition);
                return RedirectToAction("Index");
            }
            return View(taxiPosition);
        }

        //public ActionResult Reserve(int ID)
        //{

        //    if (ID == null)
        //        return RedirectToAction("Bad_Request", "Error");
        //    if (tpLogic.GetTaxiPosition(ID) != null)
        //        return View(tpLogic.GetTaxiPosition(ID));
        //    else
        //        return RedirectToAction("Not_Found", "Error");
        //}
        //[HttpPost, ActionName("Reserve")]
        //[ValidateAntiForgeryToken]
        //public ActionResult ReservationConfirmed(int ID)
        //{


        //    tpLogic.PassengerView(tpLogic.GetTaxiPosition(ID));

        //    ReservedList re = new ReservedList();

        //    return RedirectToAction("Create", "Laggage2");
        //}


        public ActionResult Reserve(int? ID)
        {
            if (ID == null)
            {
                return RedirectToAction("Bad_Request", "Error");

            }
            TaxiPosition taxiPosition = db.TaxiPosition.Find(ID);
            if (taxiPosition == null)
            {
                return HttpNotFound();
            }
            return View(taxiPosition);
        }

        // POST: ReservedLists/Delete/5
        [HttpPost, ActionName("Reserve")]
        [ValidateAntiForgeryToken]
        public ActionResult ResavationConfirmed(int ID)
        {

            ReservedList reservedList = new ReservedList();
            TaxiPosition taxiPosition = db.TaxiPosition.Find(ID);



            var po = db.reservedList.Where(n => n.TaxiNo == taxiPosition.TaxiNo && n.RouteName==taxiPosition.RouteName).Select(n => n.ReservedPassengers).FirstOrDefault();
            if (po >= 1)
            {
                po = db.reservedList.Where(n => n.TaxiNo == taxiPosition.TaxiNo).Select(n => n.ReservedPassengers).Max();

            }
            else
            {
                po = 0;
            }

            var pass = db.Passengers.ToList().Find(x => x.EmailAddress == User.Identity.Name);




            reservedList.DepertureTime = taxiPosition.DepertureTime;
            reservedList.LoadingTime = taxiPosition.LoadingTime;
            reservedList.NumSeats = taxiPosition.NumSeats;
            reservedList.RouteName = taxiPosition.RouteName;
            reservedList.RoutePrice = taxiPosition.RoutePrice;
            reservedList.TaxiNo = taxiPosition.TaxiNo;
            reservedList.DriverName = taxiPosition.DriverName;
            reservedList.OwnerTel = taxiPosition.OwnerTel;
            reservedList.DriverTel = taxiPosition.DriverTel;
            reservedList.TaxiMake = taxiPosition.TaxiMake;
            reservedList.TaxiModel = taxiPosition.TaxiModel;
            reservedList.Image = taxiPosition.Image;
            reservedList.PassengerId = pass.EmailAddress;
            reservedList.PassengerName = pass.FirstName;
            reservedList.PassengerTel = pass.PhoneNumber;
            reservedList.Date = pass.DepertureDate;
            reservedList.ReservedPassengers = po + 1;
            reservedList.AvailableSeats = taxiPosition.NumSeats - reservedList.ReservedPassengers;



            taxiPosition.ReservedCount = po + 1;

            if (reservedList.AvailableSeats!=0)
            {
              
                db.reservedList.Add(reservedList);
             
                db.SaveChanges();
                return RedirectToAction("Index", "ReservedLists");
            }
            else
            {
                TempData["AlertMessage"] = "Fully Booked";
                return View("Reserve");
            }
            
           
        }











        public ActionResult Delete(int? ID)
        {
            if (ID == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tpLogic.GetTaxiPosition(ID) != null)
                return View(tpLogic.GetTaxiPosition(ID));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int ID)
        {
            tpLogic.RemoveItem(tpLogic.GetTaxiPosition(ID));
            return RedirectToAction("Index");
        }
        public JsonResult Avail(int id)
        {
            var json = "";
            return new JsonResult();

        }
        public JsonResult GetMapPoints(string taxiId)
        {
            return Json(tpLogic.GetMapDetails(taxiId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckOut(int? ID)
        {
            if (ID == null)
                return RedirectToAction("Bad_Request", "Error");
            if (tpLogic.GetTaxiPosition(ID) != null)
                return View(tpLogic.GetTaxiPosition(ID));
            else
                return RedirectToAction("Not_Found", "Error");
        }
        [HttpPost, ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(int ID)
        {
            tpLogic.CheckOut(tpLogic.GetTaxiPosition(ID));
            return RedirectToAction("Index2");
        }


        //Payment
        #region Fields

        private readonly PayFastSettings payFastSettings;

        #endregion Fields

        #region Constructor



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
            ReservedList l = new ReservedList();
            onceOffRequest.email_address = "sbtu01@payfast.co.za";
            decimal amount = db.TaxiPosition.Select(p => p.RoutePrice).FirstOrDefault(); /*+ Convert.ToDouble(price);*/
            //var products = db.Bookings.Select(x => x.userId).ToList();to
            // Transaction Details
            onceOffRequest.m_payment_id = "";
            onceOffRequest.amount = Convert.ToDouble(amount);
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